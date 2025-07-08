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
    public class GradesService : IGradesService
    {

        private readonly IUnitOfWork _unitOfWork;
        public GradesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Grade> AddGradeAsync(Grade grade)
        {

            if (grade == null)
                throw new ArgumentNullException(nameof(grade));

            var result = await _unitOfWork.Grades.AddRecordAsync(grade);

            await _unitOfWork.Complete();
            return result;
        }

        public async Task<Grade> AddGradeAsync(int studentId, int teacherId, int subjectId, float score)
        {
            if(studentId <= 0 || teacherId <= 0 || subjectId <= 0 || score < 0)
            {
                throw new ArgumentException("Invalid parameters for adding a grade.");
            }

            var grade = new Grade
            {
                StudentId = studentId,
                TeacherId = teacherId,
                SubjectId = subjectId,
                Score = score,
                DateGrade = DateTime.Now
            };
            await _unitOfWork.Grades.AddRecordAsync(grade);
            await _unitOfWork.Complete();
            return grade;
        }

        public async Task<bool> DeleteGradeAsync(int gradeId)
        {
            var existingGrade = await  _unitOfWork.Grades.GetByIDAsync(gradeId);
            if (existingGrade != null)
            {
               await _unitOfWork.Grades.DeleteByIdAsync(gradeId);
               await _unitOfWork.Complete();
                return true;
            }
            else return false;
        }

        public async Task<bool> DeleteGradeAsync(int studentId, int subjectId)
        {
            var grade = await _unitOfWork.Grades.FindAsync(s => s.StudentId == studentId && s.SubjectId == subjectId);
            if (grade != null)
            {
                await _unitOfWork.Grades.DeleteByIdAsync(grade.Id);
                await _unitOfWork.Complete();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteGradeAsync(int studentId, int subjectId, int teacherId)
        {
            var grade = await _unitOfWork.Grades.FindAsync(s => s.StudentId == studentId && s.SubjectId == subjectId && s.TeacherId == teacherId);
            if (grade != null)
            {
                await _unitOfWork.Grades.DeleteByIdAsync(grade.Id);
                await _unitOfWork.Complete();
                return true;
            }
            return false;
        }

        public Task<Grade> GetGradeByIdAsync(int gradeId)
        {
            return _unitOfWork.Grades.GetByIDAsync(gradeId);
        }

        public async Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(int studentId)
        {
            return await _unitOfWork.Grades.FindAllAsync(s => s.StudentId == studentId,null,null);
        }

        public async Task<bool> UpdateGradeAsync(Grade grade)
        {
            var existingGrade = await _unitOfWork.Grades.GetByIDAsync(grade.Id);
            if (existingGrade != null)
            {
                await _unitOfWork.Grades.UpdateAsync(grade);
                return await _unitOfWork.Complete() > 0 ;
            }
            else
            {
                throw new Exception("Grade not found");
            }

        }
    }
}
