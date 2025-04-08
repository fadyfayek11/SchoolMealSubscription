using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Guidxus.Models;

public class Mentor
{
    [Key]
    [ForeignKey(nameof(ApplicationUser))]
    public string Id { get; set; }

    public string Specialization { get; set; } // Field of expertise for Mentors
    public string Status { get; set; } // e.g., Active, Inactive
    public ICollection<Trainee>? AssignedTrainees { get; set; }  // List of trainees
    public virtual ApplicationUser User { get; set; }

}