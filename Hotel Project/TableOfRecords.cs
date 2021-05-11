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
   public class TableOfRecords
    {
        //список,содержащий инфу 1-постоялец, 2-комната,где он живет, 3-дата занятия комнаты, 4 дата освобождения комнаты
        public List<Record> records=new List<Record>();
        public TableOfRecords() { }

    }
}
