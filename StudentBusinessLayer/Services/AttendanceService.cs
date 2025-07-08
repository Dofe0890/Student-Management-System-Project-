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
    public class AttendanceService:IAttendancesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendanceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Attendance> AddNewAttendancePerStudent(Attendance attendance)
        {
            var result = await _unitOfWork.Attendance.AddRecordAsync(attendance);
           await _unitOfWork.Complete();
            return result;
        }

        public async Task<int> CountAttendancePerStudent(int studentId)
        {
            var result = await _unitOfWork.Attendance.Count(s => s.Id == studentId);
            return result;
        }

        public async Task<bool> DeleteAttendance(int id)
        {
            var result = await  _unitOfWork.Attendance.GetByIDAsync(id);
            if (id > 0  && result != null)
            {
                await _unitOfWork.Attendance.DeleteByIdAsync(id);
                await  _unitOfWork.Complete();
                return true;

            }
            else
            return false;
        }

        public async Task<IEnumerable<Attendance>> GetAllAttendances()
        {
            return await _unitOfWork.Attendance.GetAllAsync();
        }

        public async Task<IEnumerable<Attendance>> GetAllAttendancesByDate(DateTime dateTime)
        {
            return await _unitOfWork.Attendance.FindAllAsync(s => s.Date == dateTime ,null,null);
        }

        public async Task<Attendance> GetAttendanceByDate(DateTime DateTime)
        {
            return await _unitOfWork.Attendance.FindAsync(s=> s.Date == DateTime);
        }

        public async Task<Attendance> GetAttendanceById(int id)
        {
            return await _unitOfWork.Attendance.GetByIDAsync(id);
        }

        public async Task<IEnumerable<Attendance>> GetPagedAsync(int? skip, int? take)
        {
            return await _unitOfWork.Attendance.FindAllAsync(null,skip, take);
        }
    }
}
