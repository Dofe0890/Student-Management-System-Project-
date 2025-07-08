using StudentDataAccessLayer.Models;

namespace StudentBusinessLayer.Interfaces
{
    public interface ISubjectService
    {
        Task<Subject> GetSubjectByIdAsync(int id);
        Task<Subject> GetSubjectByNameAsync(string name);
        Task<IEnumerable<Subject>> GetAllSubjectsAsync();

    }
}