using System.ComponentModel.DataAnnotations;

namespace BlazorServerApp.Models.Category
{
    internal class CategoryUIDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Last Name cannot have less than 3 characters and more than 20 characters in length")]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
