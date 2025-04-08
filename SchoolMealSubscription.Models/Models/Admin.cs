using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Guidxus.Models;

public class Admin
{
    [Key]
    [ForeignKey(nameof(ApplicationUser))]
    public string Id { get; set; }

    public bool IsRoot { get; set; } = true;
    public virtual ICollection<Semester>? Semesters { get; set; }
    public virtual ICollection<Deputy>? Deputies { get; set; }
    public virtual ApplicationUser User { get; set; }

}

public class ApplicationUser : IdentityUser
{
    public bool IsActive { get; set; }
}