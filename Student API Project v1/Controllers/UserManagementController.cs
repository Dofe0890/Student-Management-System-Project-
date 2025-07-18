﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentBusinessLayer.DTOs;
using StudentBusinessLayer.Interfaces;
using StudentBusinessLayer.Model;
using StudentDataAccessLayer.Models;

namespace StudentManagementAPI.Controllers
{
    [Authorize (Roles = "Admin")]
    [Route("api/Users")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {

        private readonly IUserManagementService _userManagementService;
        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [HttpPost("ReplaceAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RegisterAdminDTO>> ReplaceAdmin([FromBody] RegisterAdminDTO registerTeacherRequest)
        {
            if (registerTeacherRequest == null || string.IsNullOrEmpty(registerTeacherRequest.Username) || string.IsNullOrEmpty(registerTeacherRequest.FirstName) ||
                string.IsNullOrEmpty(registerTeacherRequest.LastName) || string.IsNullOrEmpty(registerTeacherRequest.Email) || string.IsNullOrEmpty(registerTeacherRequest.Password) )
            {
                return BadRequest("Invalid User data!");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userManagementService.RegisterAdminAsync(registerTeacherRequest);

            if (result == null)
                return BadRequest("Failed to save");

            return Ok(result);
        }


        [HttpPost("AddTeacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeacherDTO>> AddTeacher([FromBody] RegisterTeacherDTO registerTeacherRequest)
        {
            if (registerTeacherRequest == null || string.IsNullOrEmpty(registerTeacherRequest.Username) || string.IsNullOrEmpty(registerTeacherRequest.TeacherName) || string.IsNullOrEmpty(registerTeacherRequest.FirstName) ||
                string.IsNullOrEmpty(registerTeacherRequest.LastName) || string.IsNullOrEmpty(registerTeacherRequest.Email) || string.IsNullOrEmpty(registerTeacherRequest.Password) || registerTeacherRequest.SubjectID < 0)
            {
                return BadRequest("Invalid Teacher data!");
            }

                var result = await _userManagementService.RegisterTeacherAsync(registerTeacherRequest);

                if (result == null)
                    return BadRequest("Failed to save");

            SetRefreshTokenInCookie(result.Token, result.ExpirationOn);

            return Ok(result);
        }




       [HttpDelete("{ID}", Name = "DeleteTeacher")]
       [ProducesResponseType(StatusCodes.Status200OK)]
       [ProducesResponseType(StatusCodes.Status400BadRequest)]
       [ProducesResponseType(StatusCodes.Status404NotFound)]
       public async Task<ActionResult> DeleteTeacher(int ID)
       {
            if (ID < 1)
            {
                return BadRequest($"Not accepted ID {ID}");
            }


            if (await _userManagementService.DeleteTeacherAsync(ID))
                return Ok($"User with ID {ID} has been deleted.");
            else
                return NotFound($"User with ID {ID} not found. no rows deleted!");
        }

       

        [NonAction]
        public void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOption = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime()
            };

            Response.Cookies.Append("RefreshToken", refreshToken, cookieOption);


        }

     

    }
}
