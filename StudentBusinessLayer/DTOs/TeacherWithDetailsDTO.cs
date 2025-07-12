using StudentDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBusinessLayer.DTOs
{
    public class TeacherWithDetailsDTO
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public Subject Subject { get; set; }
        public IEnumerable<ClassroomDTO> TeacherAssignedClasses { get; set; }



    }
}
