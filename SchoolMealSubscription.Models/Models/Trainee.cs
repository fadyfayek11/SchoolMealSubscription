using System.ComponentModel.DataAnnotations.Schema;

namespace Guidxus.Models;

public class Trainee
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string EnglishFullName { get; set; }
    public string StudentNumber { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string IdentityNumber { get; set; }

    [ForeignKey(nameof(Program))]
    public int? ProgramId { get; set; }
    public TrainingProgram? Program { get; set; }


    [ForeignKey(nameof(Mentor))]
    public string? MentorId { get; set; }
    public Mentor? Mentor { get; set; }


    public ICollection<Enrollment>? Enrollments { get; set; }
    public ICollection<Attendance>? AttendanceRecords { get; set; }
}