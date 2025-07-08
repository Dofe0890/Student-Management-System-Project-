using StudentBusinessLayer.DTOs;
using StudentDataAccessLayer.Models;

namespace StudentBusinessLayer.Interfaces
{
    public interface IAttendancesService
    {
        Task<Attendance> GetAttendanceById(int id);
        Task<Attendance> GetAttendanceByDate(DateTime DateTime);
        Task<IEnumerable<Attendance>> GetAllAttendances();
        Task<IEnumerable<Attendance>> GetAllAttendancesByDate(DateTime dateTime);
        Task<IEnumerable<Attendance>> GetPagedAsync(int? skip, int? take);
        Task<bool> DeleteAttendance(int id);
        Task<Attendance> AddNewAttendancePerStudent(Attendance  attendance);
        Task<int> CountAttendancePerStudent(int studentId);

    }
}