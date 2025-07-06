using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAccessLayer.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int SubjectID { get; set; }
        public  Subject Subject { get; set; }
        public ICollection<TeacherClass> TeacherClasses { get; set; } 






    }
}
