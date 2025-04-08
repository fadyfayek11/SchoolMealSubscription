namespace Guidxus.Models;

public class Course
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public int Credits { get; set; }
    public string Level { get; set; }

    public ICollection<Enrollment>? Enrollments { get; set; }
    public ICollection<Schedule>? Schedules { get; set; }
}