using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDomainLayer.Models
{
    public  class Classroom
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; } 
        public ICollection<TeacherClass> TeacherClasses { get; set; }
    }
}
