using Meal_Ordering_Class_Library.Entities;
using Meal_Ordering_Class_Library.RequestEntitiesRestaurant;
using System.ComponentModel.DataAnnotations;

namespace Meal_Ordering_Restaurant.Models
{
    public class ManagementViewModel
    {
        public int? SelectedCategoryId { get; set; }
        public string? SelectedProductName { get; set; }
        public string? SelectedCategoryName { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public CategoryRequest? CategoryRequest { get; set; }
        public ProductRequest? ProductRequest { get; set; }
    }
}
