using Azure.Core;
using Microsoft.EntityFrameworkCore.Internal;
using StudentBusinessLayer.DTOs;
using StudentBusinessLayer.Interfaces;
using StudentBusinessLayer.Model;
using StudentDataAccessLayer.Interfaces;
using StudentDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace StudentBusinessLayer.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IAuthService _authService;
        private readonly ITeacherService _teacherService;
        private readonly IUnitOfWork _unitOfWork;
        public UserManagementService(IAuthService authService, ITeacherService teacherService,IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _teacherService = teacherService;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserWithRoleDTO> RegisterTeacherAsync(RegisterTeacherDTO dto)
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
                    SubjectID = dto.SubjectID,
                    
                };

                await _teacherService.AddNewTeacher (teacher);
                await _unitOfWork.Complete();

                await _unitOfWork.CommitTransactionAsync();

                return new UserWithRoleDTO
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

        public async Task<UserWithRoleDTO> RegisterAdminAsync(RegisterAdminDTO dto)
        {
                await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (dto != null)
                {
                    var model = new RegisterModel
                    {
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Username = dto.Username,
                        Email = dto.Email,
                        Password = dto.Password
                    };

                    var result = await _authService.RegisterAdminAsync(model);

                    if (!result.IsAuthenticated || string.IsNullOrEmpty(result.UserId))
                        throw new InvalidOperationException($"Admin registration failed: {result.Meassage}");

                    await _unitOfWork.Complete();
                    await _unitOfWork.CommitTransactionAsync();
                    return new UserWithRoleDTO
                    {
                        UserId = result.UserId,
                        ExpirationOn = result.ExpirationOn,
                        Token = result.Token,
                        TeacherId = null
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception("Registration failed", ex);
            }

        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            Teacher teacher = await  _unitOfWork.Teachers.GetByIDAsync(id);
            if (teacher == null)
                return false;


            await _unitOfWork.BeginTransactionAsync();

            try
            {
                await _teacherService.DeleteTeacher(id);
                bool deleteUser = await _authService.DeleteUserByIdAsync(teacher.UserId);

                if (!deleteUser)
                {
                    throw new Exception("Failed to delete the linked user account.");
                }
                await _unitOfWork.Complete();
                await _unitOfWork.CommitTransactionAsync();
                return true;
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception("Failed to delete teacher", ex);
            }





        }
    }
}
