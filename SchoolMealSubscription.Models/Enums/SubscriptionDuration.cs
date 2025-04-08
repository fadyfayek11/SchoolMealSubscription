using System.ComponentModel.DataAnnotations;

namespace SchoolMealSubscription.Models.Enums;

public enum SubscriptionDuration
{
    [Display(Name = "شهر واحد")]
    OneMonth = 1,

    [Display(Name = "فصل دراسي")]
    Semester = 2,

    [Display(Name = "سنة كاملة")]
    FullYear = 3
}