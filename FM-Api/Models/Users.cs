using Microsoft.AspNetCore.Identity;

namespace FM_Api.Models
{
    public class Users : IdentityUser
    {
        public string Name { get; set; }

        public string Role { get; set; }
    }
}
