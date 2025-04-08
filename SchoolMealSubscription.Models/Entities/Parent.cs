using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMealSubscription.Models.Entities;

public class Parent
{
    [Key, ForeignKey("User")] public string Id { get; set; }

    [Required(ErrorMessage = "الرجاء إدخال الاسم الكامل")]
    [Display(Name = "الاسم الكامل")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "الرجاء إدخال البريد الإلكتروني")]
    [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
    [Display(Name = "البريد الإلكتروني")]
    public string Email { get; set; }

    [Required(ErrorMessage = "الرجاء إدخال رقم الجوال")]
    [RegularExpression(@"^(05)\d{8}$", ErrorMessage = "رقم الجوال غير صحيح")]
    [Display(Name = "رقم الجوال")]
    public string PhoneNumber { get; set; }

    public virtual ICollection<Student> Students { get; set; }
    public virtual ApplicationUser User { get; set; }
}