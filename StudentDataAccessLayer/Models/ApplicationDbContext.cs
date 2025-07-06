using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAccessLayer.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TeacherClass>()
              .HasKey(tc => new { tc.TeacherID, tc.ClassroomID });

            modelBuilder .Entity<TeacherClass>()
                .HasOne(t=>t.Teacher)
                .WithMany(t=>t.TeacherClasses)
                .HasForeignKey(t=>t.TeacherID);

            modelBuilder.Entity<TeacherClass>()
               .HasOne(t => t.Classroom)
               .WithMany(t => t.TeacherClasses)
               .HasForeignKey(t => t.ClassroomID);

            modelBuilder.Entity<Teacher>()
                .HasIndex(t => t.UserId).IsUnique();


        }


        public DbSet<Student> Students {get;set;}
        public DbSet<Teacher > Teachers {get;set;}
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TeacherClass>TeacherClasses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

    }
}
