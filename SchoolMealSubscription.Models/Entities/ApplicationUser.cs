using Microsoft.AspNetCore.Identity;

namespace SchoolMealSubscription.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<Student>? Students { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
