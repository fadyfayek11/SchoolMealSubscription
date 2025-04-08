using System.ComponentModel.DataAnnotations.Schema;

namespace Guidxus.Models;

public class MentorAssignment
{
    public int Id { get; set; }

    [ForeignKey(nameof(Mentor))]
    public string? MentorId { get; set; }
    public Mentor? Mentor { get; set; }


    [ForeignKey(nameof(Trainee))]
    public int? TraineeId { get; set; }
    public Trainee? Trainee { get; set; }


    [ForeignKey(nameof(Semester))]
    public int? SemesterId { get; set; }
    public Semester? Semester { get; set; }

    public DateTime StartDate { get; set; }
}