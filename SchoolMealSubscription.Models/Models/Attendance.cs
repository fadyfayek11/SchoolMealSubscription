using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Guidxus.Models;

public class Attendance
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(Trainee))]

    public int? TraineeId { get; set; }
    public Trainee? Trainee { get; set; }
    [ForeignKey(nameof(Course))]

    public int? CourseId { get; set; }
    public Course? Course { get; set; }

    [ForeignKey(nameof(Department))]

    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }

    [ForeignKey(nameof(Specialization))]

    public int? SpecializationId { get; set; }
    public Specialization? Specialization { get; set; }



    public decimal TotalAbsencePercentage { get; set; } // إجمالي نسبة الغياب بعذر وبدون عذر
    public decimal ExcusedAbsencePercentage { get; set; } // إجمالي نسبة الغياب بعذر
    public decimal UnexcusedAbsencePercentage { get; set; } // إجمالي نسبة الغياب بدون عذر
    public decimal ExcusedTheoryAbsencePercentage { get; set; } // إجمالي نسبة غياب النظري بعذر
    public decimal ExcusedPracticalAbsencePercentage { get; set; } // إجمالي نسبة غياب العملي بعذر
    public decimal UnexcusedTheoryAbsencePercentage { get; set; } // إجمالي نسبة الغياب النظري بدون عذر
    public decimal UnexcusedPracticalAbsencePercentage { get; set; } // إجمالي نسبة الغياب العملي بدون عذر
    public int TotalAbsenceHours { get; set; } // إجمالي ساعات الغياب بعذر وبدون عذر
    public int ExcusedAbsenceHours { get; set; } // إجمالي ساعات الغياب بعذر
    public int UnexcusedAbsenceHours { get; set; } // إجمالي ساعات الغياب بدون عذر
    public int TheoryAbsenceHours { get; set; } // إجمالي غياب الساعات النظري
    public int PracticalAbsenceHours { get; set; } // إجمالي ساعات الغياب العملي
}