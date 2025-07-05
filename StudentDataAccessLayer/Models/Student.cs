using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDomainLayer.Models
{
    public class Student
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public int ClassroomId { get; set; }
        public Classroom Classroom { get; set; }
        public int Age { get; set; }
        public ICollection<Grade> Grade { get; set; }


    }
}
