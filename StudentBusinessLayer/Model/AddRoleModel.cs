using System.ComponentModel.DataAnnotations;

namespace StudentBusinessLayer.Model
{
    public class AddRoleModel
    {
        [Required]
       public string UserID {  get; set; }
        [Required]
        public string Role { get; set; }

    }
}
