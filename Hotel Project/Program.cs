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
    class Program
    {
        static void Main(string[] args)
        {

            Hotel hotel = new Hotel();

            
            Random rand_probability = new Random();
            int random_num = rand_probability.Next(1, 11);

            if (random_num == 2)
                hotel.AddRooms();
            else if (random_num == 7)
                hotel.RemoveRooms();


            Console.WriteLine("1 - забронировать номер на дату(указать диапазон брони)" +
                "\n2 - Узнать количество свободных номеров на дату(диапазон)\n" +
                "3 - Регистрация и заселение в номер\n4 - Выезд из номера\n0 - выход");

            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 0:
                    
                    Console.WriteLine("Всего доброго!");
                    break;
                case 1:
                    Console.WriteLine("ФИО:");
                    string user_fio = Console.ReadLine();

                    Console.WriteLine("Формат даты: дд/мм/гггг");
                    Console.WriteLine("Введите дату от:");
                    string booking_date_from = Console.ReadLine();
                    Console.WriteLine("Введите дату до:");
                    string booking_date_to = Console.ReadLine();
                    Console.WriteLine("--------------------------------------");

                    var dt1 = hotel.DateFormatValidation(booking_date_from);
                    var dt2 = hotel.DateFormatValidation(booking_date_to);

                    if (dt1 != DateTime.MinValue && dt2 != DateTime.MinValue)//если введенные даты нужного формата
                    {
                        List<Room> free_rooms = new List<Room>();
                           free_rooms=hotel.GetFreeRoomsOnDate(dt1, dt2);
                        
                        foreach (var r in free_rooms) 
                        {
                            Console.WriteLine($"N - {r.Room_number}\n" +
                                $"Категория - {r.Category}\n" +
                                $"Цена за ночь - {r.Price_per_night}\n" +
                                $"Количество мест - {r.Num_of_room_places}");
                            Console.WriteLine("-------------------------");
                        }
                        Console.WriteLine("Выберите из списка номер того номера, что хотите забронировать:");
                        int room_Num = int.Parse(Console.ReadLine());

                        Console.WriteLine(hotel.ReserveRoomOnDate(user_fio,room_Num,booking_date_from,booking_date_to,free_rooms));
                    }
                    else
                    {
                        Console.WriteLine("Неправильный ввод дат,попробуйте заново!");
                        break;
                    }

                    break;
                case 2:
          
                    Console.WriteLine("Формат даты: дд/мм/гггг");
                    Console.WriteLine("Введите дату от:");
                    string date_from = Console.ReadLine();
                    Console.WriteLine("Введите дату до:");
                    string date_to = Console.ReadLine();

                    Console.WriteLine(hotel.GetNumOfFreeRoomsOnDate(date_from,date_to));
                    break;
                case 3:

                    Console.WriteLine("ФИО:");
                    string guest_fio = Console.ReadLine();
                    Console.WriteLine("День рождения (дд/мм/гггг):");
                    string birthday = Console.ReadLine();
                    var bday_validate = hotel.DateFormatValidation(birthday);
                    if(bday_validate == DateTime.MinValue) 
                    {
                        Console.WriteLine("Неправильно введена дата!До свиданья!");
                        break;
                    }
                        
                    Console.WriteLine("Адресс проживания");
                    string adress = Console.ReadLine();

                    Console.WriteLine("Укажите промежуток времени,в течении которого вы будете жить в отеле\n" +
                        "Если вы уже бронировали номер, то укажите те даты брони\n(Формат даты: дд/мм/гггг)");
                    Console.WriteLine("Введите дату от:");
                    string input_date_from = Console.ReadLine();
                    Console.WriteLine("Введите дату до:");
                    string input_date_to = Console.ReadLine();
                    Console.WriteLine("--------------------------------------");

                    var Date1 = hotel.DateFormatValidation(input_date_from);
                    var Date2 = hotel.DateFormatValidation(input_date_to);

                    Record rec_with_this_user = new Record();
                    if (Date1 != DateTime.MinValue && Date2 != DateTime.MinValue)
                    {
                        rec_with_this_user = hotel.IsUserAlreadyBookedARoom(guest_fio, Date1, Date2);
                    }
                    else 
                    {
                        Console.WriteLine("Неправильный формат дат!");
                        break;
                    }
                        

                    if (rec_with_this_user!= null)
                    {
                        Console.WriteLine($"Вы уже бронировали номер {rec_with_this_user.room.Room_number} " +
                            $"на диапазон дат {rec_with_this_user.date_from} - {rec_with_this_user.date_to} ");
                        Console.WriteLine("Нужно ваше подтверждение брони:\n1 - подтверждаю,2 - нет");
                        int choose = int.Parse(Console.ReadLine());
                        switch (choose) {
                            case 1:
                                //заселение человека по предварительному бронированию
                                Console.WriteLine(hotel.GuestRegistrationWithPreviousBooking(rec_with_this_user,birthday,adress));
                                break;
                            case 2:
                                //вызов метода анулирования брони и удаления из списка records
                                hotel.BookingCanceling(rec_with_this_user);
                                Console.WriteLine("Ваша бронь была анулированна.");
                                break;
                            default:
                                Console.WriteLine("Что-то пошло не так...");
                                break;
                        }
                            
                    }
                    else//если пользователь не бронировал номер заранее то обычная регистрация
                    {
                        
                        if (Date1 != DateTime.MinValue && Date2 != DateTime.MinValue)//если введенные даты нужного формата
                        {
                            List<Room> free_rooms = new List<Room>();
                            free_rooms = hotel.GetFreeRoomsOnDate(Date1, Date2);

                            foreach (var r in free_rooms)
                            {
                                Console.WriteLine($"N - {r.Room_number}\n" +
                                    $"Категория - {r.Category}\n" +
                                    $"Цена за ночь - {r.Price_per_night}\n" +
                                    $"Количество мест - {r.Num_of_room_places}");
                                Console.WriteLine("-------------------------");
                            }
                            Console.WriteLine("Выберите из списка номер комнаты, куда хотите поселиться:");
                            int room_Num = int.Parse(Console.ReadLine());
                            string registration_res = hotel.GuestRegistrationWithoutPreviousBooking(guest_fio,adress,bday_validate,room_Num,Date1,Date2);
                            Console.WriteLine(registration_res);
                           
                            break;
                        }
                    }
                    break;
                  
                case 4:
                    Console.WriteLine("Введите ваше ФИО:");
                    string inputed_fio = Console.ReadLine();
                    Console.WriteLine("Укажите номер, в котором вы жили:");
                    int inputed_room_number = int.Parse(Console.ReadLine());

                    if (hotel.GuestCheckOut(inputed_fio, inputed_room_number))
                        Console.WriteLine("Вы покинули занимаемый вами номер. Всего доброго!");
                    else
                        Console.WriteLine("Возникли трудности с выселением...Перепроверьте ваши данные!");

                    break;
                default:
                    Console.WriteLine("Введено некорректное значение! Попробуйте еще раз.");
                    break;
            }
            //по завершению программы сериализация данных (записи и доступные комнаты)
            hotel.dataSerialization.JsonSerialize(hotel.availableRooms,hotel.av_rooms_filepath);
            hotel.dataSerialization.JsonSerialize(hotel.tableofrecords, hotel.records_filepath);
            //hotel.dataSerialization.JsonSerialize(hotel.corpuses,hotel.corpuses_filepath);
        }
    }
}
