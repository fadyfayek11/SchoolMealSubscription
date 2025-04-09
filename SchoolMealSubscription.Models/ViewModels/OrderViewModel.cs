using SchoolMealSubscription.Models.Entities;
using SchoolMealSubscription.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SchoolMealSubscription.Models.ViewModels;

public class OrderViewModel
{
    [Required(ErrorMessage = "Please select a student")]
    [Display(Name = "Student")]
    public int StudentId { get; set; }

    [Required(ErrorMessage = "Please select subscription duration")]
    [Display(Name = "Subscription Duration")]
    public SubscriptionDuration Duration { get; set; }

    [Required(ErrorMessage = "Please select payment method")]
    [Display(Name = "Payment Method")]
    public PaymentMethod PaymentMethod { get; set; }

    [Display(Name = "Additional Notes")]
    public string? AdditionalNotes { get; set; }

    // For dropdown lists
    public List<Student>? Students { get; set; }
    public List<FoodType>? FoodPreferences { get; set; }
}