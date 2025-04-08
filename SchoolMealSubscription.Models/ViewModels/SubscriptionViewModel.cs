using SchoolMealSubscription.Models.Entities;
using SchoolMealSubscription.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SchoolMealSubscription.Models.ViewModels;

public class SubscriptionViewModel
{
    // Parent Information
    [Required(ErrorMessage = "الرجاء إدخال الاسم الكامل")]
    [Display(Name = "الاسم الكامل لولي الأمر")]
    public string ParentFullName { get; set; }

    [Required(ErrorMessage = "الرجاء إدخال البريد الإلكتروني")]
    [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
    [Display(Name = "البريد الإلكتروني")]
    public string ParentEmail { get; set; }

    [Required(ErrorMessage = "الرجاء إدخال رقم الجوال")]
    [RegularExpression(@"^(05)\d{8}$", ErrorMessage = "رقم الجوال غير صحيح")]
    [Display(Name = "رقم الجوال")]
    public string ParentPhoneNumber { get; set; }

    // Student Information
    [Required(ErrorMessage = "الرجاء إدخال اسم الطالب")]
    [Display(Name = "اسم الطالب")]
    public string StudentName { get; set; }

    [Required(ErrorMessage = "الرجاء تحديد الجنس")]
    [Display(Name = "الجنس")]
    public Gender StudentGender { get; set; }

    [Required(ErrorMessage = "الرجاء اختيار الصف الدراسي")]
    [Display(Name = "الصف الدراسي")]
    public int GradeId { get; set; }

    [Required(ErrorMessage = "الرجاء اختيار المدرسة")]
    [Display(Name = "اسم المدرسة")]
    public int SchoolId { get; set; }

    // Health Information
    [Display(Name = "هل يعاني الطالب من حساسية تجاه طعام معين؟")]
    public bool HasAllergy { get; set; }

    [Display(Name = "نوع الحساسية")]
    public string AllergyDetails { get; set; }

    // Food Preferences
    [Display(Name = "تفضيلات الوجبات")]
    public List<int> SelectedFoodTypes { get; set; }

    [Display(Name = "تفضيلات أخرى")]
    public string OtherFoodPreferences { get; set; }

    // Subscription Details
    [Required(ErrorMessage = "الرجاء اختيار مدة الاشتراك")]
    [Display(Name = "مدة الاشتراك")]
    public SubscriptionDuration SubscriptionDuration { get; set; }

    [Required(ErrorMessage = "الرجاء اختيار طريقة الدفع")]
    [Display(Name = "طريقة الدفع المفضلة")]
    public PaymentMethod PaymentMethod { get; set; }

    [Display(Name = "ملاحظات إضافية")]
    public string AdditionalNotes { get; set; }

    // For dropdown lists
    public IEnumerable<Grade> Grades { get; set; }
    public IEnumerable<School> Schools { get; set; }
    public IEnumerable<FoodType> FoodTypes { get; set; }
}