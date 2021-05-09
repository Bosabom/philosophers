using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hotel_1._0
{
    class Guest
    {
        private static int amount=0;//количество постояльцев
        public int ID { get; set; }
        public string FIO { get; set; }
        public string Adress { get; set; }
        public DateTime Birthday { get; set; }

        public Guest() { }
        public Guest(string _fio, string _adress, DateTime _birth)
        {
            FIO = _fio;
            Adress = _adress;
            Birthday = _birth;
            amount++;
            ID = amount;
            
        }
    }
}
