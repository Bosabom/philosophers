﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Hotel_1._0
{
    [Serializable]
    class ListOfRooms
    {
        public List<Room> rooms=new List<Room>();//список всех номеров в отеле
        public ListOfRooms() { }

    }
}