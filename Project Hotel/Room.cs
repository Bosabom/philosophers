using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hotel_1._0
{
    
    class Room
    {
        //private static int amount_of_rooms = 0;
        //private int ID { get; set; }
        public int Room_number { get; set; }
        
        public double Price_per_night{ get; set; }
        public int Num_of_room_places { get; set; }

        public bool IsOccupied { get; set; }//свободен или занят номер

        public DateTime room_occupied_from_date { get; set; }

        public DateTime room_occupied_to_date { get; set; }


        public static HashSet<string> room_category = new() { "Стандарт", "Эконом", "Люкс", "Президентский" }; //ПЕРЕДЕЛАТЬ!!!

        private string Category { get; set; }//может enum/hashset?? - перечисление категорий:стандарт,люкс и т.д.
        
       
        //может 1 конструктор для добавления комнаты,а второй для бронирования/регистрации постояльца
        
        public Room(int _room_num, double _price_per_night, int num_of_places, string _category)
        {
            Room_number = _room_num;
            Price_per_night = _price_per_night;
            Num_of_room_places = num_of_places;
            if (room_category.Contains(_category))
            {
                Category = _category;
            }
        }

        public Room(int _room_num, double _price_per_night, int num_of_places, string _category,DateTime _date_from,DateTime _date_to)
        {
            Room_number = _room_num;
            Price_per_night = _price_per_night;
            Num_of_room_places = num_of_places;
            if (room_category.Contains(_category))
            {
                Category = _category;
            }
            room_occupied_from_date = _date_from;
            room_occupied_to_date = _date_to;

        }

        public Room() { }
    }
}
