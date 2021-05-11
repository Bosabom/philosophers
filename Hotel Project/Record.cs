using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Hotel_1._0
{
    [Serializable]
    public class Record
    {
        public Guest guest;
        public Room room;
        public DateTime date_from { get; set; }
        public DateTime date_to { get; set; }


        public Record(Guest _guest,Room _room, DateTime _date_from,DateTime _date_to)
        {
            guest = _guest;
            room = _room;
            date_from = _date_from;
            date_to = _date_to;
        }
        public Record() { }
    }
}
