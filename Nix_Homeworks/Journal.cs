using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Third_Homework
{
    class Journal
    {
        internal List<Student> students;

        public Journal()
        {
            students = new List<Student>();
            students.Add(new Student("Ivan","Ivanov","KIUKI-18-7"));
            students.Add(new Student("Valentin", "Semenov","KIUKI-18-1"));
            students.Add(new Student("Aleksandr", "Petrov","KBIKS-16-2"));
            students.Add(new Student("Dmytro", "Petrenko", "ITKN-17-4"));

            students[0].marks.AddRange(new int[] { 58, 70, 62, 40, 72 });
            students[1].marks.AddRange(new int[] { 100, 98, 90, 96, 100 });
            students[2].marks.AddRange(new int[] { 0, 36, 49, 0, 52 });
            students[3].marks.AddRange(new int[] { 96, 80, 75, 60, 84 });

        }

        public string AddStudent(string name,string surname, string group)
        {
            students.Add(new Student(name,surname,group));
            return $"Student {name} {surname} from {group} was added succesfully!";
        }
        public string AddMarkToStudent(string selected_surname, int mark)
        {
           
                foreach (var get_selected_student in students.Where((a) => a.Surname == selected_surname))
                {
                    get_selected_student.marks.Add(mark);
                }
            return $"Your mark was added succesfully!";
        }
        public double AvgMarkByStudent(string selected_student_by_surname) 
        {
            double average_mark=0;
            foreach (var get_selected_student in students.Where((a) => a.Surname == selected_student_by_surname))
            {
               average_mark=get_selected_student.marks.Average();
            }
            return average_mark;
        }
        public List<Student> BadStudents() 
        {
            List<Student> badstudents = students.Where(
                (st) => st.marks
                .Select((m)=>m<60)
                .First())
                .ToList();
            
            return badstudents;
        }

        public Dictionary<string,double> ShowJournal() 
        {
            Dictionary<string, double> journal = new Dictionary<string, double>();
            foreach(var stud in students) 
            {
                journal.Add(stud.Surname,stud.marks.Average());
            }
            return journal;
        
        }


    }
}
