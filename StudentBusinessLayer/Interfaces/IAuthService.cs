

using StudentBusinessLayer.Services;
using StudentBusinessLayer.Model;

namespace StudentBusinessLayer.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
        Task<AuthModel> RegisterAdminAsync(RegisterModel model);
        Task<bool> DeleteUserByIdAsync(string userId);
    }
}
