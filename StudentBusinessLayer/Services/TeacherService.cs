using StudentBusinessLayer.Interfaces;
using StudentDataAccessLayer.Interfaces;
using StudentDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using StudentBusinessLayer.DTOs;

namespace StudentBusinessLayer.Services
{
    public  class TeacherService:ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TeacherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Teacher> GetTeacherByName(string name)
        {
            return await _unitOfWork.Teachers.Find(s=>s.Name == name );
        }
        public async Task<Teacher> GetTeacherById(int id)
        {
            return await _unitOfWork.Teachers.Find (s=>s.Id== id);
        }
        public async Task<IEnumerable<Teacher>> GetAllTeachers()
        {
            return await _unitOfWork.Teachers.GetAll();
        }

        public async Task<Teacher> AddNewTeacher(Teacher teacher)
        {
            var exists = await _unitOfWork.Teachers.AnyAsync(t => t.UserId == teacher.UserId);

            if (!exists)
            {
                await _unitOfWork.Teachers.AddRecordAsync(teacher);
                await _unitOfWork.Complete();

                return teacher;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteTeacher(int id)
        {
            bool isDeleted = await _unitOfWork.Teachers.DeleteByIdAsync(id);

            if(isDeleted)
            {
                await _unitOfWork.Complete();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> EditTeacher(int id, TeacherDTO updatedTeacher)
        {
            var existingTeacher = await _unitOfWork.Teachers.GetByIDAsync(id);
            if (existingTeacher != null )
            {

                existingTeacher.Name = updatedTeacher.Name;
                existingTeacher.SubjectID = updatedTeacher.SubjectID;
                await _unitOfWork.Teachers.UpdateAsync(existingTeacher);
                await _unitOfWork.Complete();
                return true;
            }
            else
                return false;
        }



    }
}
