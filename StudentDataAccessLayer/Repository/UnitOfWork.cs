using Microsoft.EntityFrameworkCore.Storage;
using StudentDataAccessLayer.Interfaces;
using StudentDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAccessLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private  IDbContextTransaction _transaction;
        public IBaseRepository<Student> Students { get; private set; }
        public IBaseRepository<Teacher> Teachers { get; private set; }
        public IBaseRepository<Subject > Subjects { get; private set; }
        public IBaseRepository<Classroom> Classrooms { get; private set; }
        public IBaseRepository<TeacherClass> TeacherClasses { get; private set; }
        public IBaseRepository<Grade> Grades { get; private set; }
        public IBaseRepository<Attendance> Attendance { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Students = new BaseRepository<Student>(_context);
            Teachers = new BaseRepository<Teacher>(_context);
            Subjects = new BaseRepository<Subject>(_context);
            Classrooms = new BaseRepository<Classroom>(_context);
            TeacherClasses = new BaseRepository<TeacherClass>(_context);
            Grades = new BaseRepository<Grade>(_context);
            Attendance = new BaseRepository<Attendance>(_context);
        }

        public async Task<int> Complete()
        {
        return   await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction =await  _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _transaction?.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
          await  _transaction ?.RollbackAsync();
        }
    }
}
