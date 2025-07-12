using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAccessLayer.Models
{
    public  class Grade
    {

        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }    

        public int SubjectId { get;set; }
        public Subject Subject { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public double Score { get; set; }
        public DateTime DateGrade { get; set; } = DateTime.UtcNow;

    }
}
