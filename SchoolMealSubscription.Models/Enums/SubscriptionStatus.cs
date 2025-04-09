using System.ComponentModel.DataAnnotations;

namespace SchoolMealSubscription.Models.Enums;

public enum SubscriptionStatus
{
    [Display(Name = "Pending")]
    Pending = 1,

    [Display(Name = "Paid")]
    Paid = 2,

    [Display(Name = "Cancelled")]
    Cancelled = 3
}