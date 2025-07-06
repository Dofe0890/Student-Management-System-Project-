using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentBusinessLayer.DTOs;
using StudentDataAccessLayer.Models;

namespace StudentBusinessLayer.Interfaces
{
    public interface IStudentService
    {
        Task<Student> GetStudentById(int id);
        Task<bool> EditStudent(int id, StudentDTO UpdatedStudent);
        Task<bool> DeleteStudent(int id);
        Task<Student>AddNewStudent(Student student);
        Task<IEnumerable <Student>> GetAllStudents();
        Task<Student> GetStudentByName(string name);
        Task<IEnumerable<Student>> GetPassedStudents(int? skip , int? take);
        Task<IEnumerable<Student>> GetStudentsByAgeOrder(int? skip, int? take );

    }
}
