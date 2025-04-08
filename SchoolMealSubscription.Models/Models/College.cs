namespace Guidxus.Models;

public class College
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }

    public ICollection<Department>? Departments { get; set; }
}