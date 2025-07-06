using Microsoft.EntityFrameworkCore.Internal;
using StudentBusinessLayer.DTOs;
using StudentBusinessLayer.Interfaces;
using StudentBusinessLayer.Model;
using StudentDataAccessLayer.Interfaces;
using StudentDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace StudentBusinessLayer.Services
{
    internal class RegistrationService : IRegistrationService
    {
        private readonly IAuthService _authService;
        private readonly ITeacherService _teacherService;
        private readonly IUnitOfWork _unitOfWork;
        public RegistrationService(IAuthService authService, ITeacherService teacherService,IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _teacherService = teacherService;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserWithTeacherDTO> RegisterTeacherAsync(RegisterTeacherDTO dto)
        {
            var RegisterModel = new RegisterModel 
            { 
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Username = dto.Username,
            Email = dto.Email,
            Password = dto.Password,

            };

           await  _unitOfWork.BeginTransactionAsync();
            try
            {
                var authResult = await _authService.RegisterAsync(RegisterModel);

                if (!authResult.IsAuthenticated || string.IsNullOrEmpty(authResult.UserId))
                    throw new InvalidOperationException($"User registration failed: {authResult.Meassage}");
 
                var teacher = new Teacher
                {
                    UserId = authResult.UserId ,
                    Name = dto.TeacherName,
                    SubjectID = dto.SubjectID
                };

                await _teacherService.AddNewTeacher (teacher);
                await _unitOfWork.Complete();

                await _unitOfWork.CommitTransactionAsync();

                return new UserWithTeacherDTO
                {
                    UserId = authResult.UserId,
                    ExpirationOn = authResult.ExpirationOn,
                    TeacherId = teacher.Id,
                    Token = authResult.Token
                   
                };
            }
            catch (Exception ex)
            {
             await _unitOfWork.RollbackTransactionAsync();
                throw new Exception("Registration failed", ex);
            }


        }

        public Task<UserWithTeacherDTO> RegisterAdminAsync(RegisterTeacherDTO dto)
        {
            throw new NotImplementedException();
        }

    }
}
