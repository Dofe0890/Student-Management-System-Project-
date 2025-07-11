using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentDataAccessLayer.Interfaces;
using StudentDataAccessLayer.Repository;
using StudentDataAccessLayer.Models;
using StudentDataAccessLayer;
using StudentBusinessLayer.Interfaces;
using StudentBusinessLayer.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using StudentBusinessLayer.Model;
namespace StudentBusinessLayer.Services
{
    public class StudentService : IStudentService
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public StudentService (IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Student> AddNewStudent(Student student)
        {
           
            if (student != null)
            {
                var result = await _unitOfWork.Students.AddRecordAsync(student);
                await _unitOfWork.Complete();
                return result  ;


            }
            else
            {
                return null; 
            }
          
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var student  = await  _unitOfWork.Students.GetByIDAsync(id);
            if(student != null)
            {
                await _unitOfWork.Students.DeleteByIdAsync(id);
                await _unitOfWork.Complete();
                return true;


            }
            else
            {
                return false;
            }
        }

        public async Task<bool> EditStudent(int id, StudentDTO UpdatedStudent)
        {
            var student = await _unitOfWork.Students.GetByIDAsync(id);


            if (student == null)
                return false;
            student.Name = UpdatedStudent.Name;
            student.Age = UpdatedStudent.Age;
            student.ClassroomId = UpdatedStudent.ClassroomId;
            
            await _unitOfWork.Students.UpdateAsync(student);
            return await _unitOfWork.Complete() > 0;
        }


        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _unitOfWork.Students.GetAllAsync();
        }

       public async Task<Student> GetStudentById(int id)
        {
            return await _unitOfWork .Students.GetByIDAsync(id);
        }

        public Task<IEnumerable<Student>> GetStudentsByAgeOrder(int? skip, int? take)
        {
             return _unitOfWork .Students.FindAllAsync(s=>s.ID>0 , skip , take ,b=>b.Age , "DESC");           
        }
    


    }
}
