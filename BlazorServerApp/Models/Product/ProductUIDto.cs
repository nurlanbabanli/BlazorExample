using System.ComponentModel.DataAnnotations;

namespace BlazorServerApp.Models.Product
{
    internal class ProductUIDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "Last Name cannot have less than 3 characters and more than 20 characters in length")]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ShopFavorites { get; set; }
        [Required(ErrorMessage = "Please enter color")]
        [StringLength(maximumLength:20, MinimumLength = 3, ErrorMessage = "Last Name cannot have less than 3 characters and more than 20 characters in length")]
        public string Color { get; set; }
        public string ImageUrl { get; set; }
        //[Required(ErrorMessage = "Please select category")]
        [Range(minimum:1,int.MaxValue,ErrorMessage ="Please select category")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
