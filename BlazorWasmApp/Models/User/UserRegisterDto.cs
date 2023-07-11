using System.ComponentModel.DataAnnotations;

namespace BlazorWasmApp.Models.User
{
    internal class UserRegisterDto
    {
        [Required(ErrorMessage ="Please enter Email")]
        //[EmailAddress(ErrorMessage ="Please enter valid email")]
        [RegularExpression(@"^((?!\.)[\w-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$", ErrorMessage = "Please enter valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please enter Password")]
        [StringLength(maximumLength:100, MinimumLength =3, ErrorMessage ="Password must be more than 3 characters")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Please enter FirstName")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Please enter LastName")]
        public string LastName { get; set; }
    }
}
