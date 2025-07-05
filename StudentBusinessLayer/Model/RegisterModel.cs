using System.ComponentModel.DataAnnotations;

namespace StudentBusinessLayer.Model
{
    public class RegisterModel
    {
        [Required ,StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(100)]
        public string Password { get; set; }

        [Required, StringLength(256)]
        public string Email { get; set; }




    }
}
