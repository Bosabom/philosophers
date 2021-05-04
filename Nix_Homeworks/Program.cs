using System;
using System.Collections.Generic;
using System.Linq;
namespace Nix_Third_Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Journal journal_of_studs=new Journal();
            Console.WriteLine("Menu:\n1-Add student\n2-Add mark to student\n" +
                "3-Calculate average mark for student\n4-Show list of bad students\n5-Show journal\n0-Exit");

            int choice = int.Parse(Console.ReadLine());
            while (choice != 6)
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Name:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Surname:");
                        string surname = Console.ReadLine();
                        Console.WriteLine("Group:");
                        string group = Console.ReadLine();
                        Console.WriteLine(journal_of_studs.AddStudent(name, surname, group)); 
                        Console.WriteLine("Add mark for new student:\nInput a mark in diapazon [0,100]");
                        int mark_for_new_stud = int.Parse(Console.ReadLine());
                        if (mark_for_new_stud >= 0 && mark_for_new_stud <= 100)
                            Console.WriteLine(journal_of_studs.AddMarkToStudent(surname,mark_for_new_stud));
                        else
                            Console.WriteLine("Something goes wrong...");

                        
                        Console.WriteLine("Menu:\n1-Add student\n2-Add mark to student\n" +
                  "3-Calculate average mark for student\n4-Show list of bad students\n5-Show journal\n6-Exit");
                        choice = int.Parse(Console.ReadLine());
                        break;

                    case 2:

                        Console.WriteLine("List of students:");
                        foreach (var studs in journal_of_studs.students)
                        {
                            Console.WriteLine(studs.Surname);
                        }
                        Console.WriteLine("Enter '1' to add mark\nEnter '0' when you want to exit.");
                        int choose = int.Parse(Console.ReadLine());
                        while (choose != 0)
                        {
                            Console.WriteLine("Input student's surname:");
                            string inputed_surname = Console.ReadLine();
                            Console.WriteLine("Input a mark in diapazon [0,100]");
                            int inputed_mark = int.Parse(Console.ReadLine());
                            if (inputed_mark >= 0 && inputed_mark <= 100)
                            {
                                Console.WriteLine(journal_of_studs.AddMarkToStudent(inputed_surname, inputed_mark));
                                Console.WriteLine("Enter '1' to add mark\nEnter '0' when you want to exit.");
                                choose = int.Parse(Console.ReadLine());

                            }
                            else
                            {
                                Console.WriteLine("There is a wrong value of inputted number! Please try again!");
                            }
                        }

                        Console.WriteLine("Menu:\n1-Add student\n2-Add mark to student\n" +
                 "3-Calculate average mark for student\n4-Show list of bad students\n5-Show journal\n6-Exit");
                        choice = int.Parse(Console.ReadLine());
                        break;
                    case 3:

                        Console.WriteLine("List of students:");
                        foreach (var studs in journal_of_studs.students)
                        {
                            Console.WriteLine(studs.Surname);
                        }
                        Console.WriteLine("Input a surname of student for calculation his average mark:");
                        string stud_surname = Console.ReadLine();
                        Console.WriteLine($"Average mark = {journal_of_studs.AvgMarkByStudent(stud_surname)}");

                        Console.WriteLine("Menu:\n1-Add student\n2-Add mark to student\n" +
                 "3-Calculate average mark for student\n4-Show list of bad students\n5-Show journal\n6-Exit");
                        choice = int.Parse(Console.ReadLine());
                        break;
                    case 4:
                        List<Student> bad_students = journal_of_studs.BadStudents();
                        foreach (var st in bad_students)
                        {
                            Console.WriteLine(st.Surname);
                        }

                        Console.WriteLine("Menu:\n1-Add student\n2-Add mark to student\n" +
                 "3-Calculate average mark for student\n4-Show list of bad students\n5-Show journal\n6-Exit");
                        choice = int.Parse(Console.ReadLine());
                        break;
                    case 5:
                        Console.WriteLine("Student's surname - average mark:");
                        Dictionary<string, double> journal = journal_of_studs.ShowJournal();

                        foreach (KeyValuePair<string, double> surname_and_avg_mark in journal)
                        {
                            Console.WriteLine(surname_and_avg_mark.Key + " - " + surname_and_avg_mark.Value);
                        }

                        Console.WriteLine("Menu:\n1-Add student\n2-Add mark to student\n" +
                 "3-Calculate average mark for student\n4-Show list of bad students\n5-Show journal\n6-Exit");
                        choice = int.Parse(Console.ReadLine());
                        break;
                       
                    default:
                        Console.WriteLine("Incorrect input!!!");

                        break;
                }
            }
        }
    }
}
