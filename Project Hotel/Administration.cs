using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
namespace Hotel_1._0
{
    class Administration
    {
        ListOfRooms list_of_rooms;
        ListOfGuests list_of_guests;

        private Dictionary<int, int> guests_rooms;
        public Administration()
        {

            guests_rooms = new Dictionary<int, int>();//key - id постояльца, value - номер комнаты ПОДУМАТЬ!!!
            list_of_guests = new ListOfGuests();
            list_of_rooms = new ListOfRooms();


            list_of_guests.guests.Add(new Guest("Братищенко Н.Р.", "Харьков, пр.Победы,70", new DateTime(2000, 7, 12)));
            list_of_guests.guests.Add(new Guest("Зайцев Д.Я.", "Полтава, ул. Мира,21", new DateTime(2001, 6, 11)));
            list_of_guests.guests.Add(new Guest("Лысенко Б.С.", "Полтава, пр. Независимости,34", new DateTime(1998, 4, 9)));
            list_of_guests.guests.Add(new Guest("Шевченко Д.Ю", "Харьков, пр. Науки,16", new DateTime(1990, 1, 30)));
            list_of_guests.guests.Add(new Guest("Просяник А.С.", "Харьков, пр.Людвига Свободы, 51", new DateTime(1984, 8, 17)));
            list_of_guests.guests.Add(new Guest("Ноженко Д.И.", "Изюм, ул. Центральная, 14", new DateTime(1970, 12, 23)));

            
            list_of_rooms.rooms.Add(new Room(101,467.99,2,"Люкс"));
            list_of_rooms.rooms.Add(new Room(102, 239.50, 2, "Стандарт"));
            list_of_rooms.rooms.Add(new Room(103, 199.99, 3, "Эконом",new DateTime(2021,5,7),new DateTime(2021,5,14)));
            list_of_rooms.rooms.Add(new Room(104, 239.50, 3, "Стандарт",new DateTime(2021, 6, 8), new DateTime(2021, 6, 20)));
   

            list_of_rooms.rooms.Add(new Room(105, 99.99, 1, "Эконом", new DateTime(2021, 6, 21), new DateTime(2021, 6, 30)));
            list_of_rooms.rooms.Add(new Room(106, 149.99, 2, "Эконом", new DateTime(2021, 8, 1), new DateTime(2021, 8, 13)));
            list_of_rooms.rooms.Add(new Room(107, 199.99, 3, "Эконом", new DateTime(2021, 8, 7), new DateTime(2021, 8, 16)));


            //guests_rooms.Add(list_of_guests.guests[0].ID,list_of_rooms.rooms[0].Room_number);

        }

        public void AddRoom() { }//пока хз каким образом добавлять

        public void RemoveRoom() { }//пока хз каким образом удалять

        public void ReserveRoomOnDate()
        {


        }

        public string GetNumOfFreeRoomsOnDate(string date_from,string date_to) 
        {
            DateTime dt1,dt2;
            var IsValidDate1 = DateTime.TryParse(date_from, out dt1);
            var IsValidDate2 = DateTime.TryParse(date_to, out dt2);
           
            if(IsValidDate1 && IsValidDate2)
            {
                //узнаем кол-ство свободных номеров в этом диапазоне
              
                var num_of_free_rooms_by_date = list_of_rooms.rooms.Count((r)=>
                (dt1 > r.room_occupied_to_date || //1-ая дата выбранная(от) > даты,до которой номер занят
                dt2 < r.room_occupied_from_date) ||//2-ая дата выбранная(до) < даты, начиная с которой номер занят
                //если комната не была забронирована на любую дату
                (r.room_occupied_from_date == DateTime.MinValue && 
                r.room_occupied_to_date == DateTime.MinValue));

                return $"Количество свободных комнат на указанные вами даты = {num_of_free_rooms_by_date}";
            }
            else            
                return "Неправильный формат введенных дат!";
        }

        public void GuestRegistration() { }

        public void GuestCheckIn() { }

    }
}
