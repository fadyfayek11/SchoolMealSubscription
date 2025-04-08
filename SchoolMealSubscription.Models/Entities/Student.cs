using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;
using SchoolMealSubscription.Models.Enums;

namespace SchoolMealSubscription.Models.Entities;

public class Student
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "الرجاء إدخال اسم الطالب")]
    [Display(Name = "اسم الطالب")]
    public string Name { get; set; }

    [Required(ErrorMessage = "الرجاء تحديد الجنس")]
    [Display(Name = "الجنس")]
    public Gender Gender { get; set; }

    [Required(ErrorMessage = "الرجاء اختيار الصف الدراسي")]
    [Display(Name = "الصف الدراسي")]
    public int GradeId { get; set; }

    [ForeignKey("GradeId")]
    public virtual Grade Grade { get; set; }

    [Required(ErrorMessage = "الرجاء اختيار المدرسة")]
    [Display(Name = "اسم المدرسة")]
    public int SchoolId { get; set; }

    [ForeignKey("SchoolId")]
    public virtual School School { get; set; }

    [Display(Name = "هل يعاني من حساسية؟")]
    public bool HasAllergy { get; set; }

    [Display(Name = "نوع الحساسية")]
    public string AllergyDetails { get; set; }

    public string ParentId { get; set; }

    [ForeignKey("ParentId")]
    public virtual Parent Parent { get; set; }

    // Navigation property
    public virtual ICollection<Subscription> Subscriptions { get; set; }
    public virtual ICollection<StudentFoodPreference> FoodPreferences { get; set; }
}