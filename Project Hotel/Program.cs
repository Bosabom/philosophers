using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Hotel_1._0
{
    class Program
    {
        static void Main(string[] args)
        {

            Administration adminka = new Administration();
            // Console.Write();
            //adminka.GetNumOfFreeRoomsOnDate("9/6/2021", "25/6/2021");
            //Console.WriteLine("1 - забронировать номер на дату(указать диапазон брони)" +
            //    "\n2 - Узнать количество свободных номеров на дату(диапазон)\n" +
            //    "3 - Регистрация и заселение в номер\n4 - Выезд из номера\n0 - выход");

            //int choice = int.Parse(Console.ReadLine());
            //switch (choice)
            //{
            //    case 0:
            //        Console.WriteLine("Всего доброго!");
            //        break;
            //    case 1:
            //        break;
            //    case 2:
            //        break;
            //    case 3:
            //        break;
            //    case 4:
            //        break;
            //    default:
            //        Console.WriteLine("Введено неккоректное значение! Попробуйте еще раз.");
            //        break;
            //}

            //DateTime date1 = new DateTime(2010,7,8);
            //DateTime date2 = new DateTime(2012, 7, 8);
            //Console.WriteLine(date2 - date1);
        }
    }
}
