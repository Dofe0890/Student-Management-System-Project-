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
                return null;


            var studentExists = await _unitOfWork.Students.FindAsync(s => s.ID == grade.StudentId);
            var teacherExists = await _unitOfWork.Teachers.FindAsync(t => t.Id == grade.TeacherId);
            var subjectExists = await _unitOfWork.Subjects.FindAsync(su => su.Id == grade.SubjectId);

            if (studentExists == null || teacherExists == null || subjectExists == null)
            {
                return null;
            }




            var isGradeExist = await _unitOfWork.Grades.FindAsync(g => g.StudentId == grade.StudentId && g.SubjectId == grade.SubjectId && g.TeacherId == grade.TeacherId);

            if (isGradeExist != null)
            {
                return null;
            }


            var result = await _unitOfWork.Grades.AddRecordAsync(grade);

            await _unitOfWork.Complete();
            return result;
        }

        public async Task<Grade> AddGradeAsync(int studentId, int teacherId, int subjectId, double score)
        {
            if(studentId <= 0 || teacherId <= 0 || subjectId <= 0 || score <= 0)
            {
                return null;
            }

            var studentExists = await _unitOfWork.Students.FindAsync(s => s.ID  == studentId);
            var teacherExists = await _unitOfWork.Teachers.FindAsync(t => t.Id == teacherId);
            var subjectExists = await _unitOfWork.Subjects.FindAsync(su => su.Id == subjectId);

            if (studentExists == null || teacherExists == null || subjectExists == null)
            {
                return null;
            }




            var result = await _unitOfWork.Grades.FindAsync(g => g.StudentId == studentId && g.SubjectId == subjectId && g.TeacherId == teacherId);

            if(result  != null)
            {
                return null; 
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

        public async Task<IEnumerable<Grade>> GetAllGradesAsync()
        {
            return await _unitOfWork.Grades.GetAllAsync();
        }

        public async Task<Grade> GetGradeByIdAsync(int gradeId)
        {
            var result = await _unitOfWork.Grades.Query().Include(g => g.Student)
                               .Include(g => g.Subject)
                               .Include(g => g.Teacher)
                               .FirstOrDefaultAsync(g => g.Id == gradeId);

            return result; 
        }

        public async Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(int studentId)
        {
            var result = await _unitOfWork.Grades.Query().Include(g => g.Student)
                             .Include(g => g.Subject)
                             .Include(g => g.Teacher)
                             .Where(g => g.StudentId == studentId).ToListAsync();
            return result;
        }

        public async Task<bool> UpdateGradeAsync(Grade grade)
        {
            var existingGrade = await _unitOfWork.Grades.GetByIDAsync(grade.Id);
            if (existingGrade != null)
            {
                existingGrade.Score = grade.Score;
                existingGrade.DateGrade = grade.DateGrade;
                existingGrade.StudentId = grade.StudentId;
                existingGrade.SubjectId = grade.SubjectId;
                existingGrade.TeacherId = grade.TeacherId;



                await _unitOfWork.Grades.UpdateAsync(existingGrade);
                return await _unitOfWork.Complete() > 0 ;
            }
            else
            {
                return false;
            }

        }
    }
}
