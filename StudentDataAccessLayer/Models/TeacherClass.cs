﻿using System.ComponentModel.DataAnnotations;

namespace StudentDataAccessLayer.Models
{
    public class TeacherClass
    {
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }

        public int ClassroomID { get; set; }
        public Classroom Classroom {get;set;}

        public DateTime AssignedDate { get; set; }
    }
}