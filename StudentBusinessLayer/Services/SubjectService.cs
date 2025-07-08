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
    public class SubjectService:ISubjectService 
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _unitOfWork.Subjects.GetAllAsync();
        }

        public async Task<Subject> GetSubjectByIdAsync(int id)
        {
            return await _unitOfWork.Subjects.GetByIDAsync(id);
        }
        public async Task<Subject> GetSubjectByNameAsync(string name)
        {
            return await _unitOfWork.Subjects.FindAsync(s=>s.Name == name);
        }


    }
}
