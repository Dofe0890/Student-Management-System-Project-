using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using StudentBusinessLayer.DTOs;
using StudentDataAccessLayer.Models;

namespace StudentBusinessLayer.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Student,StudentDTO>().ReverseMap();
            CreateMap<Teacher,TeacherDTO>().ReverseMap();
            CreateMap<Classroom, ClassroomDTOWithDetails>().ForMember(dest => dest.Teachers, opt => opt.MapFrom(src => src.TeacherClasses.Select(tc => tc.Teacher)));
            CreateMap<Classroom, ClassroomDTO>().ReverseMap();
            CreateMap< Attendance ,AttendanceDTO>().ReverseMap();

        }
    }
}
