using System.ComponentModel.DataAnnotations;

namespace SchoolMealSubscription.Models.Entities;

public class School
{
    [Key]
    public int SchoolId { get; set; }

    [Required]
    [Display(Name = "اسم المدرسة")]
    public string? Name { get; set; }
}