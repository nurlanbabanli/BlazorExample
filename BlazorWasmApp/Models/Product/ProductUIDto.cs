using System.ComponentModel.DataAnnotations;

namespace BlazorWasmApp.Models.Product
{
    internal class ProductUIDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ShopFavorites { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
