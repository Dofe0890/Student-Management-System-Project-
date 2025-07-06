using StudentBusinessLayer.DTOs;
using StudentDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBusinessLayer.Interfaces
{
    public interface ITeacherService
    {
        Task<Teacher> GetTeacherById(int id);
        Task<bool> EditTeacher(int id, Teacher updatedTeacher);
        Task<bool> DeleteTeacher(int id);
        Task<Teacher> AddNewTeacher(Teacher  teacher);
        Task<IEnumerable<Teacher>> GetAllTeachers();
        Task<Teacher> GetTeacherByName(string name);

    }
}
