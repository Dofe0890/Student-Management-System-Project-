using Microsoft.EntityFrameworkCore;
using StudentBusinessLayer.Interfaces;
using StudentDataAccessLayer.Interfaces;
using StudentDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBusinessLayer.Services
{
    public class ClassroomService : IClassroomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClassroomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> AssignTeacherAsync(int classroomId, int teacherId)
        {
            var exist = await _unitOfWork.TeacherClasses.AnyAsync(c=>c.TeacherID == teacherId && c.ClassroomID == classroomId);
            if (!exist)
            {
                var result = await _unitOfWork.TeacherClasses.AddRecordAsync(new TeacherClass
                {
                    ClassroomID = classroomId,
                    TeacherID = teacherId
                });
               await _unitOfWork.Complete();
                return true;
            }
            else
               return false;
                    
        }

        public async Task<Classroom> CreateClassroomAsync(Classroom classroom)
        {

            if (classroom != null)
            {
                var result = await _unitOfWork.Classrooms.AddRecordAsync(classroom);
                await _unitOfWork.Complete();
                return classroom;
            }
            else
            {
                return null;
            }

        }

        public async Task<bool> DeleteClassroomAsync(int classroomId)
        {
            var existClass = await _unitOfWork.Classrooms.GetByIDAsync(classroomId);
            if (existClass != null)
            {
                var result = await _unitOfWork.Classrooms.DeleteByIdAsync(classroomId);
                await _unitOfWork.Complete();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<Classroom>> GetAllClassesAsync()
        {
            return await _unitOfWork.Classrooms.GetAllAsync();
        }

        public async Task<Classroom> GetClassByIdWithDetails(int classroomId)
        {
            return await _unitOfWork.Classrooms.Query()
                .Include(c => c.Students)
                .Include(c => c.TeacherClasses)
                .ThenInclude(tc => tc.Teacher).FirstOrDefaultAsync(s => s.Id == classroomId);
        }

        public async Task<Classroom> GetClassByName(string name)
        {
           return await _unitOfWork.Classrooms.FindAsync(c => c.Name == name);
        }
    }
}
