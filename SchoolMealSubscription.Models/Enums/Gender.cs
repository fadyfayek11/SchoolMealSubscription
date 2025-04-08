using System.ComponentModel.DataAnnotations;

namespace SchoolMealSubscription.Models.Enums;


public enum Gender
{
    [Display(Name = "ذكر")]
    Male = 1,

    [Display(Name = "أنثى")]
    Female = 2
}