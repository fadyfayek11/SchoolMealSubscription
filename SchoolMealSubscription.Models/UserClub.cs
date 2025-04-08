
namespace SchoolMealSubscription.Models
{
    public class UserClub
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}
