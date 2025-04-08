namespace Guidxus.Models;

public class Section
{
    public int Id { get; set; }
    public string Number { get; set; }
    public List<Attendance>? Attendances { get; set; } 
}