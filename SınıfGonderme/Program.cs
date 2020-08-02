using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Build.Execution;

namespace SınıfGonderme
{
    
    class Program
    {
         [STAThread]
        static void Main(string[] args)
        {
            //COM port ayarlarımız
            SerialPort mySerialPort = new SerialPort("COM5");
            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.RtsEnable = true;
            //Veri geldiğinde çalışıcak metotu burada sınıfımıza ekliyoruz.
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            mySerialPort.Open();
            Application.Run();
            Console.ReadKey();
        }
        private static void DataReceivedHandler(object sender,SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //Veri bu değişkenin içine geliyor.
            string indata = sp.ReadExisting();
            //Burada projeyi tekrar derliyoruz
            /*Burada projeyi derlememin sebebi program çalıştığında Sınıfı gönderdiğimiz programla beraber Sınıfı gönderdiğimiz programa sınıfı eklemeden önceki haliyle 
             beraber derlenir,bu da doğal olarak yeni eklediğimiz sınıfın görülmemesine neden olur.Sınıfı burada tekrar derleyerek yeni eklediğimiz sınıfın gözükmesini sağlıyoruz.
             'ProjectInstance' metotunun içine yeniden derlemek istediğimiz '.csproj' dosyamızın yolunu parametre olarak gönderiyoruz.*/
            var proje = new ProjectInstance(@"E:\Visual\SınıfGonderme\SınıfGonderme\SınıfGonderme.csproj");
            //Projeyi Build metotu kullanrak derliyoruz.
            proje.Build();

            Console.WriteLine("Data Received:");
            Console.Write(indata.ToString());
            Console.WriteLine(indata);
            //Gelen veriler Base64String'e çevrilmiş olduğu için FromBase64String metotunu kullnaıp Base64String formatından sınıfımızı tekrardan byte dizi haline dönüştürüyoruz.
            byte[] sınıf = Convert.FromBase64String(indata);
            //FromBase64String metotu kullnarak elde ettiğimiz byte diziyi,byte dizi'den object'e metotunu kullanrak object hale dönüştürüyoruz.Ve bir object değişkenin içerisine atıyoruz.
            object a = ByttanObjecte(sınıf);
           Console.WriteLine( a.GetType());
            Console.WriteLine(a);
            //Ben sınıfı object bir generic class kullanarak kaydetmeyi seçtim.
            SınıfKaydetme<object> SınıfKaydet = new SınıfKaydetme<object>(a);

        }
        //Byte dizi olan verilerimizi Object hale dönüştüren metot.
        public static object ByttanObjecte(byte[] Byteler)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(Byteler, 0, Byteler.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Console.WriteLine(memStream);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;

        }
       
    }
}
