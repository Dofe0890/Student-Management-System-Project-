using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBusinessLayer.DTOs
{
    public  class UserWithTeacherDTO
    {
        public string UserId { get; set; }
        public int TeacherId { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationOn { get; set; }

    }
}
