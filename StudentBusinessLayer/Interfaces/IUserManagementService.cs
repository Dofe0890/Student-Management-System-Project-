using StudentBusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBusinessLayer.Interfaces
{
    public interface IUserManagementService
    {
        Task<UserWithRoleDTO> RegisterTeacherAsync(RegisterTeacherDTO dto);
        Task<UserWithRoleDTO> RegisterAdminAsync(RegisterAdminDTO dto);
        Task<bool> DeleteTeacherAsync(int id);


    }
}
