using StudentDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBusinessLayer.DTOs
{
    public  class AttendanceDTO
    {
        public int StudentId { get; set; }
        public bool IsPresent { get; set; }

    }
}
