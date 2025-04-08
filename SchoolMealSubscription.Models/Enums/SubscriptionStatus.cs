using System.ComponentModel.DataAnnotations;

namespace SchoolMealSubscription.Models.Enums;

public enum SubscriptionStatus
{
    [Display(Name = "قيد الانتظار")]
    Pending = 1,

    [Display(Name = "مدفوع")]
    Paid = 2,

    [Display(Name = "ملغي")]
    Cancelled = 3
}