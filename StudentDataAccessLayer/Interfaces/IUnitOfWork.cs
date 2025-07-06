using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentDataAccessLayer.Models;
namespace StudentDataAccessLayer.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IBaseRepository<Student> Students { get; }
        IBaseRepository<Teacher > Teachers { get; }
        IBaseRepository<Subject> Subjects { get;}
        IBaseRepository<Classroom> Classrooms { get; }
        IBaseRepository<TeacherClass> TeacherClasses { get; }
        IBaseRepository<Grade> Grades { get; }
        IBaseRepository<Attendance> Attendance { get; }
        Task<int>  Complete();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

    }
}
