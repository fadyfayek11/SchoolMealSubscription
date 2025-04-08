using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Guidxus.Models;

public class DepartmentHead
{
    [Key]
    [ForeignKey(nameof(ApplicationUser))]
    public string Id { get; set; }

    [ForeignKey(nameof(Department))]
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }
    public ICollection<Mentor>? Mentors { get; set; } // Mentors managed by this Head
    public virtual ApplicationUser User { get; set; }

}