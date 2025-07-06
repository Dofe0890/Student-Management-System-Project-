using StudentBusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBusinessLayer.Interfaces
{
    public interface IRegistrationService
    {
        Task<UserWithTeacherDTO> RegisterTeacherAsync(RegisterTeacherDTO dto);
        Task<UserWithTeacherDTO> RegisterAdminAsync(RegisterTeacherDTO dto);
    }
}
