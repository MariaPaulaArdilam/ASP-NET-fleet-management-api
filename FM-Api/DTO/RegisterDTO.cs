using System.ComponentModel.DataAnnotations;

namespace FM_Api.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
