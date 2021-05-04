using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Third_Homework
{
    class Student
    {
        private string Name { get; set; }
        internal string Surname { get; set; }
        private string Group { get; set; }

        internal List <int> marks;//Dictionary or List???

        public Student(string _name, string _surname, string _group)
        {
            Name = _name;
            Surname = _surname;
            Group = _group;
            marks = new List<int>();
        }
    }
}
