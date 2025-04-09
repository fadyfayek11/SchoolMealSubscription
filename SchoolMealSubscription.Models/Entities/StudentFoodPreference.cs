using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolMealSubscription.Models.Entities;

public class StudentFoodPreference
{
    [Key]
    public int Id { get; set; }

    public int StudentId { get; set; }

    [ForeignKey("StudentId")]
    public virtual Student? Student { get; set; }

    public int FoodTypeId { get; set; }

    [ForeignKey("FoodTypeId")]
    public virtual FoodType? FoodType { get; set; }
}