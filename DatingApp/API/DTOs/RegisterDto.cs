using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Hey dummy, you need to have a username!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Hey dummy, you need to have a Password!!")]
        public string Password { get; set; }
    }
}