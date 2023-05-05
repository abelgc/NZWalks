using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "User Name is required")]
        [DataType(DataType.EmailAddress)]
        public string? Username { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
