using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StudentDomainLayer.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
        public int SubjectID { get; set; }
        public  Subject Subject { get; set; }
        public ICollection<TeacherClass> TeacherClasses { get; set; } 






    }
}
