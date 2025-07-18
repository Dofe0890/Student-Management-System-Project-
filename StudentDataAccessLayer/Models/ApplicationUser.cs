﻿
using Microsoft.AspNetCore.Identity;
using StudentDataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentDataAccessLayer.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required,MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }
        public List<RefreshToken>? RefreshTokens { get; set; }


    }
}
