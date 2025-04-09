using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SchoolMealSubscription.Models.Enums;

namespace SchoolMealSubscription.Models.Entities;

public class Student
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Name")]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "Gender")]
    public Gender Gender { get; set; }

    [Required]
    [Display(Name = "Grade Id")]
    public int GradeId { get; set; }

    [ForeignKey("GradeId")]
    public virtual Grade? Grade { get; set; }

    [Required]
    [Display(Name = "School Id")]
    public int SchoolId { get; set; }

    [ForeignKey("SchoolId")]
    public virtual School? School { get; set; }

    [Display(Name = "HasAllergy")]
    public bool HasAllergy { get; set; }

    [Display(Name = "AllergyDetails")]
    public string? AllergyDetails { get; set; }

    public string? ParentId { get; set; }

    [ForeignKey("ParentId")]
    public virtual ApplicationUser? Parent { get; set; }

    // Navigation property
    public virtual ICollection<StudentFoodPreference> FoodPreferences { get; set; }
}