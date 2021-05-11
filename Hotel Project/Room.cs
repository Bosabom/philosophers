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
    public class Room
    {
       
        public int Room_number { get; set; }
        
        public double Price_per_night{ get; set; }
        public int Num_of_room_places { get; set; }

        public bool IsOccupied { get; set; }//свободен или занят номер
        public string Category { get; set; }
        
        
        public Room(int _room_num, double _price_per_night, int num_of_places, string _category)
        {
            Room_number = _room_num;
            Price_per_night = _price_per_night;
            Num_of_room_places = num_of_places;
            Category = _category;
            IsOccupied = false;
            
        }
        public Room() { }
    }
}
