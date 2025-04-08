using System.ComponentModel.DataAnnotations;

namespace SchoolMealSubscription.Models.Enums;

public enum PaymentMethod
{
    [Display(Name = "مدى")]
    Mada = 1,

    [Display(Name = "Apple Pay")]
    ApplePay = 2,

    [Display(Name = "STC Pay")]
    StcPay = 3,

    [Display(Name = "تحويل بنكي")]
    BankTransfer = 4
}