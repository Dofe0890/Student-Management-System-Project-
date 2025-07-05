using StudentDomainLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBusinessLayer.DTOs
{
    public class StudentDTO
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Range(6, 100)]
        public int Age { get; set; }

        public int ClassroomId { get; set; }

       
    }
}
