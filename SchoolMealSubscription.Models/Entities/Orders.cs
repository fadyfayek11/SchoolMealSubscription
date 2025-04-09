using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SchoolMealSubscription.Models.Enums;

namespace SchoolMealSubscription.Models.Entities;

public class Orders
{
    [Key]
    public int Id { get; set; }

    public string ParentId { get; set; }

    [ForeignKey("ParentId")]
    public virtual ApplicationUser Parent{ get; set; }
    
    public int StudentId { get; set; }

    [ForeignKey("StudentId")]
    public virtual Student Student{ get; set; }

    [Required]
    [Display(Name = "Duration")]
    public SubscriptionDuration Duration { get; set; }

    [Required]
    [Display(Name = "Payment Method")]
    public PaymentMethod PaymentMethod { get; set; }

    [Display(Name = "Additional Notes")]
    public string? AdditionalNotes { get; set; }

    [Display(Name = "Subscription Date")]
    public DateTime SubscriptionDate { get; set; } = DateTime.Now;

    [Display(Name = "Subscription Status")]
    public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Pending;

    [Display(Name = "Invoice Number")]
    public string? InvoiceNumber { get; set; }
    
    public decimal? Amount { get; set; }
}