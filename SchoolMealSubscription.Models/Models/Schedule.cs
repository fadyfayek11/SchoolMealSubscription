using System.ComponentModel.DataAnnotations.Schema;

namespace Guidxus.Models;

public class Schedule
{
    public int Id { get; set; }

    [ForeignKey(nameof(Course))]
    public int? CourseId { get; set; }
    public Course? Course { get; set; }


    [ForeignKey(nameof(Mentor))]
    public string? MentorId { get; set; }
    public Mentor? Mentor { get; set; }


    public string Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Room { get; set; }
    public string Building { get; set; }
}