using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO.Ports;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ReflecitonTemel
{  
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //COM port ayarları
            SerialPort mySerialPort = new SerialPort("COM4");
            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.RtsEnable = true;
            mySerialPort.Open();
            //Hazırladığımız sınıfı tanımlama 
            Personel personel = new Personel();
            Console.WriteLine(personel);
            //Yazdığımız Objecten byte'a metotunu kullanarak object nesne olan sınıfımızı dizi haline getiriyoruz.
            byte[] dizi = ByteDonusturucu(personel);
            //Bir çok farklı yol mevcut fakat ben ToBase64String metotunu kullanmayı tercih ettim daha önce byte dizi haline getirdiğimiz sınıfımızı burada ToBase64String kullanarak 
            //String bir ifadeye çeviriyoruz.
            string s = Convert.ToBase64String(dizi);
           mySerialPort.Write(s); 
            Console.WriteLine("Devam etmek için bir tuşa basın");
            Console.ReadKey();
            mySerialPort.Close();
            Console.ReadLine();
        }

        //Object olan nesneyi byte dizi haline dönüştürmeye yarayan metot.
        public static byte[] ByteDonusturucu(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream mr = new MemoryStream();
            bf.Serialize(mr, obj);
            return mr.ToArray();

        }

    }

}
