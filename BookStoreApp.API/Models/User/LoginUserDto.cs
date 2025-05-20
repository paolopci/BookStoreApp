using System.ComponentModel.DataAnnotations;


namespace BookStoreApp.API.Models.User
{
    public class LoginUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

    }
}
