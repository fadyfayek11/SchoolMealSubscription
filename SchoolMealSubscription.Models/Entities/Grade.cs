using System.ComponentModel.DataAnnotations;

namespace SchoolMealSubscription.Models.Entities;

public class Grade
{
    [Key]
    public int GradeId { get; set; }

    [Required]
    [Display(Name = "الصف الدراسي")]
    public string Name { get; set; }
}