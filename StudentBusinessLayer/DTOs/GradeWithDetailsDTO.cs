using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBusinessLayer.DTOs
{
    public class GradeWithDetailsDTO
    {


            public int StudentId { get; set; }
            public StudentDTO Student { get; set; }

            public int SubjectId { get; set; }
            public SubjectDTO Subject { get; set; }

            public int TeacherId { get; set; }
            public TeacherDTO Teacher { get; set; }

            public double Score { get; set; }
            public DateTime DateGrade { get; set; }
    }
}



