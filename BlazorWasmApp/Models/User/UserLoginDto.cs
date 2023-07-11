using System.ComponentModel.DataAnnotations;

namespace BlazorWasmApp.Models.User
{
    internal class UserLoginDto
    {
        [Required(ErrorMessage = "Please enter Email")]
        [RegularExpression(@"^((?!\.)[\w-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$", ErrorMessage = "Please enter valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }
    }
}
