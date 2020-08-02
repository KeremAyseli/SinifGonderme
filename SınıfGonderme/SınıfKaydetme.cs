using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
namespace SınıfGonderme
{
    //gelen sınıfı json dosyasına kaydetmeye yarayan sınıf.
    class SınıfKaydetme<T>
    {
        public SınıfKaydetme(T veri)
        {
            Console.WriteLine("TİPİ " + veri); StreamWriter yazma = new StreamWriter(Environment.CurrentDirectory + "veri.json");
            string json = JsonConvert.SerializeObject(veri);
            yazma.Write(json);
            yazma.Close();
        }
    }
}
