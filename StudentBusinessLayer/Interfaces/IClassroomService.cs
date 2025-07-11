using StudentDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBusinessLayer.Interfaces
{
    public interface IClassroomService
    {
        Task<Classroom> CreateClassroomAsync(Classroom classroom);
        Task<bool> AssignTeacherAsync(int classroomId , int teacherId);
        Task<IEnumerable<Classroom>> GetAllClassesAsync();
        Task<Classroom> GetClassByIdWithDetails(int classroomId);
        Task<Classroom> GetClassByName(string name);
        Task<bool> DeleteClassroomAsync(int classroomId);
        Task<bool> EditClassroomAsync(int classroomId , string newClassroomName);
    }
}
