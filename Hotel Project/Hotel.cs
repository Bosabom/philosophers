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
    class Hotel
    {
        public TableOfRecords tableofrecords=null;
       
        public AvailableRooms availableRooms=null;

        public Corpuses corpuses=null;
        
        Random rand = new Random();
        private int rand_corp_num;

        internal DataSerialization dataSerialization;

        internal string corpuses_filepath = @"D:\Apps(c#)\Hotel 1.0\data files\corpuses.json";
        internal string av_rooms_filepath = @"D:\Apps(c#)\Hotel 1.0\data files\available rooms.json";
        internal string records_filepath = @"D:\Apps(c#)\Hotel 1.0\data files\records.json";
        
        
        //public static HashSet<string> room_category = new() { "Стандарт", "Эконом", "Люкс", "Президентский" }; 
        public Hotel()
        {
            dataSerialization = new DataSerialization();

            rand_corp_num = rand.Next(1, 4);//рандомизация номера корпуса чтоб с некоторой вероятностью его закрыть/открыть

            tableofrecords = new TableOfRecords();
            
            corpuses = dataSerialization.JsonDeserialize(typeof(Corpuses),corpuses_filepath) as Corpuses;
            


            availableRooms = dataSerialization.JsonDeserialize(typeof(AvailableRooms),av_rooms_filepath)as AvailableRooms;

            tableofrecords = dataSerialization.JsonDeserialize(typeof(TableOfRecords), records_filepath) as TableOfRecords;


            
            foreach (var r in tableofrecords.records)
                r.room.IsOccupied = true;
        }

        public void AddRooms()
        {
            
            //проверка на то что такие номера с этого корпуса НЕ в списке доступных,и только тогда добавить

            foreach (var room_in_corp in corpuses.corps[rand_corp_num])
            {
                if (!availableRooms.av_rooms.Contains(room_in_corp))
                    availableRooms.av_rooms.Add(room_in_corp);
            }
        }
        public void RemoveRooms() 
        {
            bool isAllRoomsAreFree = corpuses.corps[rand_corp_num].All(r=>r.IsOccupied == false);//все ли номера не заняты?
            if (isAllRoomsAreFree)
            {
                //availableRooms.av_rooms.RemoveAll(room=>corpuses[corp_num_for_closing].Contains(room));
                availableRooms.av_rooms = availableRooms.av_rooms.Except(corpuses.corps[rand_corp_num]).ToList();
            }
        }

        public string ReserveRoomOnDate(string FIO, int room_N, string date_from, string date_to,List<Room> free_rooms_on_date)//для бронирования достаточно ФИО, и даты с и по какую бронь
        {
            var dt1 = DateFormatValidation(date_from);
            var dt2 = DateFormatValidation(date_to);

            if (dt1 != DateTime.MinValue && dt2 != DateTime.MinValue)
            {
                foreach (var r in free_rooms_on_date.Where((room) => room.Room_number == room_N))//проверка правильности ввода номера
                {
                    r.IsOccupied = true;
                    tableofrecords.records.Add(new Record(new Guest(FIO), r, dt1, dt2));//если все ок то бронируем этот номер

                    using (StreamWriter sw = new StreamWriter((@"D:\Apps(c#)\Hotel 1.0\data files\bookings.txt"), true, Encoding.UTF8))
                    {
                        sw.WriteLine($"{FIO} забронировал {r.Room_number} номер на {date_from} - {date_to}");
                    }
                }
                return $"Номер был успешно забронирован!";
            }
            else
                return "Перепроверьте формат введенных данный.";


        }

        public string GetNumOfFreeRoomsOnDate(string date_from, string date_to) //узнаем кол-ство свободных номеров в этом диапазоне
        {
            var dt1 = DateFormatValidation(date_from);
            var dt2 = DateFormatValidation(date_to);

            if (dt1 != DateTime.MinValue && dt2 != DateTime.MinValue)//если введенные даты нужного формата
            {
                int num_of_free_rooms = GetFreeRoomsOnDate(dt1, dt2).Count;

                return $"Количество свободных комнат на указанные вами даты = {num_of_free_rooms}";
            }
            else
                return "Неправильный формат введенных дат!";
        }

        public List<Room> GetFreeRoomsOnDate(DateTime date_from, DateTime date_to) //узнаем кол-ство свободных номеров в этом диапазоне
        {
            List<Room> free_rooms = new List<Room>();

            //проверяем каждый номер-занят ли он или нет
            foreach (var room in availableRooms.av_rooms)
            {
                if (!room.IsOccupied)
                    free_rooms.Add(room);
            }

            var free_rooms_by_date = tableofrecords.records.Where((rec) => date_from > rec.date_to || date_to < rec.date_from);//проверка по датам в журнале                

            foreach (var rooms in free_rooms_by_date)
            {
                free_rooms.Add(rooms.room);
            }

            return free_rooms;

        }

        public Record IsUserAlreadyBookedARoom(string FIO,DateTime dt1,DateTime dt2)
        {
            foreach (var r in tableofrecords.records.Where(rec => rec.guest.FIO == FIO && rec.date_from == dt1 && rec.date_to == dt2))
            {
                return r;
            }
            return null;
        }

        internal DateTime DateFormatValidation(string data_for_validation)
        {
            DateTime date;
            var IsValidDate = DateTime.TryParse(data_for_validation, out date);
            if (IsValidDate)
                return date;
            else
                return DateTime.MinValue;//если провалилась валидация(дата не соответствует формату)
        }
        public string GuestRegistrationWithPreviousBooking(Record current_guest_r, string bday, string Adress)
        {
            var right_format_bday = DateFormatValidation(bday);
            if (right_format_bday != DateTime.MinValue)
            {
                current_guest_r.guest.Adress = Adress;
                current_guest_r.guest.Birthday = right_format_bday;

                using (StreamWriter sw = new StreamWriter((@"D:\Apps(c#)\Hotel 1.0\data files\checkin-checkout.txt"), true, Encoding.UTF8))
                {
                    sw.WriteLine($"{current_guest_r.guest.FIO} поселился в номере {current_guest_r.room.Room_number} " +
                        $"на {current_guest_r.date_from} - {current_guest_r.date_to} (предварительное бронирование)");
                }
                return $"Вы были заселены в номер {current_guest_r.room.Room_number}. Приятного отдыха!";

            }
            return "Неправильный формат введенных данных!";
        }
        public string GuestRegistrationWithoutPreviousBooking(string FIO, string Adress, DateTime Birthday, int N_room, DateTime date_from, DateTime date_to)
        {
            
            List<Room> available_rooms = new List<Room>();
            available_rooms = GetFreeRoomsOnDate(date_from, date_to);

            foreach (var room in available_rooms.Where((room) => room.Room_number == N_room))//проверка правильности ввода номера
            {
                room.IsOccupied = true;
                tableofrecords.records.Add(new Record(new Guest(FIO,Adress,Birthday), room, date_from, date_to));//если все ок то заселяем в этот номер

                using (StreamWriter sw = new StreamWriter((@"D:\Apps(c#)\Hotel 1.0\data files\checkin-checkout.txt"), true, Encoding.UTF8))
                {
                    sw.WriteLine($"{FIO} поселился в {room.Room_number} номер на {date_from} - {date_to}");
                }
                return $"Вы были заселены в {room.Room_number} номер! Приятного отдыха!";
            }
            return $"Возникли трудности с обработкой данных..Перепроверьте введенные вами данные!";

        }
        public void BookingCanceling(Record rec_with_this_guest) 
        {
            tableofrecords.records.Remove(rec_with_this_guest);
            using (StreamWriter sw = new StreamWriter((@"D:\Apps(c#)\Hotel 1.0\data files\bookings.txt"), true, Encoding.UTF8))
            {
                sw.WriteLine($"Бронь анулирована ({rec_with_this_guest.guest.FIO} , номер {rec_with_this_guest.room.Room_number}, " +
                    $"{rec_with_this_guest.date_from} - {rec_with_this_guest.date_to} )");
            }
        }
        public bool GuestCheckOut(string fio,int room_num) 
        {
            foreach(var get_this_record in tableofrecords.records.Where((r) => r.guest.FIO == fio && r.room.Room_number == room_num))
            {
                get_this_record.room.IsOccupied = false;
                tableofrecords.records.Remove(get_this_record);
                using (StreamWriter sw = new StreamWriter((@"D:\Apps(c#)\Hotel 1.0\data files\checkin-checkout.txt"), true, Encoding.UTF8))
                {
                    sw.WriteLine($"{fio} выехал из номера {get_this_record.room.Room_number}");
                }
                return true;
            }
            return false;
          
        }

    }
}
