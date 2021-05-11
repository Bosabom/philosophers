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
    public class AvailableRooms
    {
        public List<Room> av_rooms = new List<Room>();
        public AvailableRooms() { }
    }
}
