using System.Data;
using System.Text.Json.Serialization;

namespace StudentBusinessLayer.Model
{
    public class AuthModel
    {
        public  string Meassage { get; set; }
        public bool IsAuthenticated {  get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
         public DateTime ExpirationOn { get; set; }

        [JsonIgnore]
        public string ? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }

    }
}
