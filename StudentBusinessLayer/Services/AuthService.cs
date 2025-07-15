using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentBusinessLayer.Helper;
using StudentBusinessLayer.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using StudentDataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace StudentBusinessLayer.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt; 
       public AuthService(UserManager<ApplicationUser> userManager ,RoleManager<IdentityRole>roleManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                return new AuthModel { Meassage = "Email is already registered" };
            }
            if (await _userManager.FindByNameAsync(model.Username) != null)
            {
                return new AuthModel { Meassage = "Username is already registered" };
            }

            var User = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName

            };

            var result = await _userManager.CreateAsync(User, model.Password);

            if (!result.Succeeded)
            {
                var Errors = string.Empty;

                foreach (var error in result.Errors)
                {
                    Errors += $"{error.Description},";
                }
                return new AuthModel { Meassage = Errors };
            }


            await _userManager.AddToRoleAsync(User, "User");

            var jwtSecurityToken = await CreateJwtToken(User);

            var refreshToken = GenerateRefreshToken();
            User.RefreshTokens?.Add(refreshToken);
            await _userManager.UpdateAsync(User);

            return new AuthModel
            {
                Email = User.Email,
                ExpirationOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = User.UserName,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpireOn,
                UserId = User.Id
            };

        }

        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel { };

            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user == null|| !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Meassage = "Email or Password is incorrect!"; ;
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username  = user.UserName;
            authModel.ExpirationOn = jwtSecurityToken.ValidTo;


            var roleList = await _userManager.GetRolesAsync(user);
            authModel.Roles = roleList .ToList();


            if(user.RefreshTokens.Any(t=>t.IsActive))
            {
                var actionRefreshToken = user.RefreshTokens.FirstOrDefault(s => s.IsActive);
                authModel.RefreshToken = actionRefreshToken.Token;
                authModel.RefreshTokenExpiration = actionRefreshToken.ExpireOn;
             
            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                authModel.RefreshToken = refreshToken.Token;
                authModel.RefreshTokenExpiration = refreshToken.ExpireOn;
                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);

            }

            return authModel;
        }

        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByIdAsync(model.UserID);

            if(user==null)
            {
                return "Invalid UserId please try valid Id";
            }

            if(!await _roleManager.RoleExistsAsync(model.Role))
                return "Invalid Role please try valid role";

            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User is already assigned to this role ";

            var result = await _userManager.AddToRoleAsync(user, model.Role);



            return result.Succeeded ? "Its Done Successfully" : "Something went wrong ";


        }

        public async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync (user);
            var roles = await _userManager .GetRolesAsync(user);
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList ();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString ()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id)
            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurityKay = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKay,SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer
                , audience: _jwt.Audience
                , claims: claims
                , expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMintues)
                , signingCredentials: signingCredentials);

                return jwtSecurityToken;

        }

        public  async Task<AuthModel> RefreshTokenAsync(string token)
        {
            var authModel = new AuthModel();

            var user = await _userManager.Users .SingleOrDefaultAsync(u => u.RefreshTokens.Any(t=>t.Token == token));

            if (user == null)
            {
                authModel.IsAuthenticated = false;
                authModel.Meassage = "Invalid token";
                return authModel;
            }

            var refreshToken = user.RefreshTokens.SingleOrDefault (t=> t.Token == token);

            if(refreshToken == null || !refreshToken .IsActive)
            {
                authModel.IsAuthenticated = false;
                authModel.Meassage = "Invalid token";
                return authModel;
            }

            refreshToken.RevokedOn = DateTime.UtcNow ;

            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);


            var jwtToken = await CreateJwtToken(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken (jwtToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            var roles = await _userManager.GetRolesAsync(user);
            authModel .Roles = roles.ToList();
            authModel.RefreshToken = newRefreshToken.Token;
            authModel.RefreshTokenExpiration = newRefreshToken.ExpireOn;
            authModel.ExpirationOn = jwtToken.ValidTo;
            authModel.UserId = user.Id;
            return authModel;


        }

        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using( var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomNumber);
            }

            return new RefreshToken()
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpireOn = DateTime.Now.AddDays(10),
                CreateTime = DateTime.UtcNow

            };
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var authModel = new AuthModel();

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
                return false;

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
                return false;

            refreshToken.RevokedOn = DateTime.UtcNow;


            await _userManager.UpdateAsync(user);



            return true;
        }

        public async Task<AuthModel> RegisterAdminAsync(RegisterModel model)
        {
            var existingAdmin = await _userManager.GetUsersInRoleAsync("Admin");

            foreach (var admin in existingAdmin)
            {
              var isDeleted = await _userManager.DeleteAsync(admin);
                if (!isDeleted.Succeeded)
                {
                    // Optionally log or throw
                    var errors = string.Join(", ", isDeleted.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to delete user {admin.UserName}: {errors}");
                }
            }

            var result = await RegisterAsync(model);
            if(!result.IsAuthenticated )
                return result;

            var user = await _userManager.FindByIdAsync (result .UserId);
            await _userManager.AddToRoleAsync(user, "Admin");
            return result;
        }

        public async Task<bool> DeleteUserByIdAsync(string userId)
        {
            var user = await  _userManager.FindByIdAsync (userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                return result.Succeeded;

            }
            else
                return false;
             
        }
    }
}
