using System.ComponentModel.DataAnnotations;

namespace SchoolMealSubscription.Models.Entities;

public class FoodType
{
    [Key]
    public int FoodTypeId { get; set; }

    [Required]
    public string? Name { get; set; }

    public decimal Price { get; set; }
}