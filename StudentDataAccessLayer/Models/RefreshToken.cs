﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAccessLayer.Models
{
    [Owned]
    public  class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpireOn {  get; set; }
        public bool IsExpired => DateTime.UtcNow > ExpireOn;
        public DateTime? CreateTime { get; set; }
        public DateTime? RevokedOn { get; set; }
        public bool IsActive =>  RevokedOn == null && !IsExpired;

    }
}
