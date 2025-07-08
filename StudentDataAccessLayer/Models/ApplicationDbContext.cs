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

            modelBuilder.Entity<Subject>().HasData(
    new Subject { Id = 1, Name = "Mathematics", Description = "Study of numbers, algebra, geometry, and calculus." },
    new Subject { Id = 2, Name = "Physics", Description = "Study of matter, energy, and their interactions." },
    new Subject { Id = 3, Name = "Chemistry", Description = "Study of substances and chemical reactions." },
    new Subject { Id = 4, Name = "Biology", Description = "Study of living organisms." },
    new Subject { Id = 5, Name = "English", Description = "Language and literature studies." },
    new Subject { Id = 6, Name = "History", Description = "Study of past events." },
    new Subject { Id = 7, Name = "Geography", Description = "Study of Earth's surface and environment." },
    new Subject { Id = 8, Name = "Computer Science", Description = "Study of programming, data, and systems." },
    new Subject { Id = 9, Name = "Islamic Studies", Description = "Study of Islamic religion and teachings." },
    new Subject { Id = 10, Name = "Social Studies", Description = "Study of human society and social relationships." },
    new Subject { Id = 11, Name = "Art", Description = "Visual and creative arts." },
    new Subject { Id = 12, Name = "Physical Education", Description = "Exercise, fitness, and sports." },
    new Subject { Id = 13, Name = "Science", Description = "General science including basic biology, chemistry, and physics." }
                );


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
