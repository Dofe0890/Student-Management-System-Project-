using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using StudentBusinessLayer.Model;
using StudentBusinessLayer.Services;

namespace StudentManagementAPI.Controllers
{ 
    [Route("api/[controller]")]
        [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
       
        public AuthController (IAuthService authService)
        {
            _authService = authService;
        }



        [HttpPost("GetToken")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel Model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.GetTokenAsync(Model);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Meassage);
            }

            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration );

            return Ok(result);
        }

       

        [NonAction]
      public void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOption = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime ()
            };

            Response.Cookies.Append ("RefreshToken",refreshToken,cookieOption);


        }


        [HttpGet ("RefreshToken")]
        public async Task<IActionResult> RefreashToken ()
        {
            var refreshToken = Request.Cookies["RefreshToken"];
            var result = await _authService.RefreshTokenAsync(refreshToken);
            if (!result.IsAuthenticated)
                return BadRequest(result.Meassage);

            SetRefreshTokenInCookie (result .RefreshToken , result.RefreshTokenExpiration);

            return Ok(result);
        }


        [HttpPost ("RevokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeToken model )
        {
            var token = model.Token ?? Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest("Token is required!");

            var result = await _authService.RevokeTokenAsync (token);

            if (!result)
                return BadRequest("Token is invalid!");

            return Ok("Token revoke successfully"); 

        }


    }
}
