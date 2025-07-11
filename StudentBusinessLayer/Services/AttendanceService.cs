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
    public class AttendanceService:IAttendancesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendanceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Attendance> AddNewAttendancePerStudent(Attendance attendance)
        {
            var isStudentExist = await _unitOfWork.Students.GetByIDAsync(attendance.StudentId);
            if(isStudentExist == null )
            {
                return null;
            }
            var result = await _unitOfWork.Attendance.AddRecordAsync(attendance);
           await _unitOfWork.Complete();
            return result;
        }

        public async Task<int> CountAttendancePerStudent(int studentId)
        {
            var result = await _unitOfWork.Attendance.Count(s => s.StudentId == studentId && s.IsPresent == true);
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

        public async Task<IEnumerable<Attendance>> GetAllAttendancesByDate(DateTime dateTime, string filterType)
        {
            IQueryable<Attendance> query = _unitOfWork.Attendance.Query();

            switch (filterType.ToLower())
            {
                case "day":
                    query = query.Where(x => x.Date.Date == dateTime.Date);
                    break;
                case "month":
                    query = query.Where(a => a.Date.Month == dateTime.Month && a.Date.Year == dateTime.Year);
                    break;
                case "year":
                    query = query.Where(a => a.Date.Year == dateTime.Year);
                    break;

                default:
                    throw new ArgumentException("Invalid filter type. Use 'day', 'month', or 'year'.");
            }

            return await query.ToListAsync();

        }

        public async Task<IEnumerable<Attendance>> GetAttendanceByDatePerStudent(int studentId, DateTime DateTime, string filterType)
        {
           IQueryable<Attendance> query = _unitOfWork.Attendance.Query()
                .Where(a => a.StudentId == studentId);
            switch (filterType.ToLower())
            {
                case "day":
                    query = query.Where(x => x.Date.Date == DateTime.Date);
                    break;
                case "month":
                    query = query.Where(a => a.Date.Month == DateTime.Month && a.Date.Year == DateTime.Year);
                    break;
                case "year":
                    query = query.Where(a => a.Date.Year == DateTime.Year);
                    break;
                default:
                    throw new ArgumentException("Invalid filter type. Use 'day', 'month', or 'year'.");
            }
            return await query.ToListAsync();
        }

        public async Task<Attendance> GetAttendanceById(int id)
        {
            return await _unitOfWork.Attendance.GetByIDAsync(id);
        }

        public async Task<IEnumerable<Attendance>> GetPagedAsync(int? skip, int? take)
        {
            return await _unitOfWork.Attendance.FindAllAsync(s=>s.IsPresent,skip, take);
        }
    }
}
