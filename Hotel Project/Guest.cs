using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hotel_1._0
{
    [Serializable]
    public class Guest
    {
        
        public static int guests_quantity=0;//количество постояльцев
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
            guests_quantity++;
            ID = guests_quantity;
        }
        //конструктор для создания пользователя в случае бронирования номера
        public Guest(string _fio_)
        {
            FIO = _fio_;
            guests_quantity++;
            ID = guests_quantity;
        }
    }
}
