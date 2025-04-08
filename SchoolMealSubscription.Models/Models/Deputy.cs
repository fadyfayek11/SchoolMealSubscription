using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Guidxus.Models;

public class Deputy
{

    [Key]
    [ForeignKey(nameof(ApplicationUser))]
    public string Id { get; set; }

    public int? CollegeId { get; set; }
    public College? College { get; set; }
    public ICollection<DepartmentHead>? DepartmentHeads { get; set; } // Departments overseen
    public ICollection<Department>? Departments { get; set; } // Departments overseen
    public virtual ApplicationUser User { get; set; }

}