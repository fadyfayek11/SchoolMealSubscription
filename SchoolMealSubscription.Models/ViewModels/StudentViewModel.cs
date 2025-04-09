using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolMealSubscription.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SchoolMealSubscription.Models.ViewModels;

public class StudentViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [Display(Name = "Full Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Gender is required")]
    [Display(Name = "Gender")]
    public Gender Gender { get; set; }

    [Required(ErrorMessage = "Grade is required")]
    [Display(Name = "Grade")]
    public int GradeId { get; set; }

    [Required(ErrorMessage = "School is required")]
    [Display(Name = "School")]
    public int SchoolId { get; set; }

    [Display(Name = "Has Allergy")]
    public bool HasAllergy { get; set; }

    [Display(Name = "Allergy Details")]
    [StringLength(500, ErrorMessage = "Allergy details cannot exceed 500 characters")]
    public string? AllergyDetails { get; set; }

    // For dropdown lists
    public IEnumerable<SelectListItem>? Grades { get; set; }
    public IEnumerable<SelectListItem>? Schools { get; set; }

    [Required(ErrorMessage = "Food Preferences is required")]
    [Display(Name = "Food Preferences")]
    public List<int> SelectedFoodTypeIds { get; set; } = new List<int>();

    public List<FoodTypeOption> AvailableFoodTypes { get; set; } = new List<FoodTypeOption>();
}


public class FoodTypeOption
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsSelected { get; set; }
}