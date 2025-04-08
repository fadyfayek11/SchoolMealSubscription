using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SchoolMealSubscription.Models.Enums;

namespace SchoolMealSubscription.Models.Entities;

public class Subscription
{
    [Key]
    public int SubscriptionId { get; set; }

    public int StudentId { get; set; }

    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; }

    [Required(ErrorMessage = "الرجاء اختيار مدة الاشتراك")]
    [Display(Name = "مدة الاشتراك")]
    public SubscriptionDuration Duration { get; set; }

    [Required(ErrorMessage = "الرجاء اختيار طريقة الدفع")]
    [Display(Name = "طريقة الدفع")]
    public PaymentMethod PaymentMethod { get; set; }

    [Display(Name = "ملاحظات إضافية")]
    public string AdditionalNotes { get; set; }

    [Display(Name = "تاريخ الاشتراك")]
    public DateTime SubscriptionDate { get; set; } = DateTime.Now;

    [Display(Name = "حالة الاشتراك")]
    public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Pending;

    // For invoice generation
    [Display(Name = "رقم الفاتورة")]
    public string InvoiceNumber { get; set; }
}