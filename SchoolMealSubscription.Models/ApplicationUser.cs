using Microsoft.AspNetCore.Identity;

namespace SchoolMealSubscription.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsAdmin { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Club> OwnedClubs { get; set; }
    }
}
