using System.ComponentModel.DataAnnotations;

namespace SchoolMealSubscription.Models.Entities;

public class FoodType
{
    [Key]
    public int FoodTypeId { get; set; }

    [Required]
    [Display(Name = "نوع الطعام")]
    public string Name { get; set; }
}