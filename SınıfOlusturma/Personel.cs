using System;

namespace ReflecitonTemel
{
    //Sınıfımızda bulunan en önemli noktalaradan bir tanesi bu,sınıfımızı serileştirilebilir hale getiriyoruz.
    [Serializable]
    class Personel
    {
        public int Numara { get; set; }
        public string İsim { get; set; }
        public string Soyİsim { get; set; }
        public string Departman { get; set; }
        public string[] Uzmanlıkları { get; set; }  
    }
}
