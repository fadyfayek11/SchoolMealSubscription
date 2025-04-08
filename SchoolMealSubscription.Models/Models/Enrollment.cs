namespace Guidxus.Models;

public class Enrollment
{
    public int Id { get; set; }

    public int? TraineeId { get; set; }
    public Trainee? Trainee { get; set; }

    public int? CourseId { get; set; }
    public Course? Course { get; set; }

    public int? SemesterId { get; set; }
    public Semester? Semester { get; set; }

    public string Grade { get; set; }
    public double GPA { get; set; }
}