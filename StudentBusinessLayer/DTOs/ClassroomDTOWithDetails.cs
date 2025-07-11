using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBusinessLayer.DTOs
{
    public class ClassroomDTOWithDetails
    {
        public string Name { get; set; }
        public IEnumerable<StudentDTO> Students { get; set; }
        public IEnumerable<TeacherDTO> Teachers { get; set; }
    }
}
