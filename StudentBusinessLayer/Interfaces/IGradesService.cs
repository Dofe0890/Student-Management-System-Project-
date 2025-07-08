using StudentDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBusinessLayer.Interfaces
{
    public interface IGradesService
    {
        Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(int studentId);
        Task<Grade> GetGradeByIdAsync(int gradeId);
        Task<Grade> AddGradeAsync(Grade grade);
        Task<Grade> AddGradeAsync(int studentId ,int teacherId , int subjectId , float score);
        Task<bool> UpdateGradeAsync(Grade grade);
        Task<bool> DeleteGradeAsync(int gradeId);
        Task<bool> DeleteGradeAsync(int studentId,int subjectId);
        Task<bool> DeleteGradeAsync(int studentId , int subjectId, int teacherId);

    }
}
