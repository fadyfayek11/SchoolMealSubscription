using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMealSubscription.Models;

public class Club
{
    [Key]
    public int Id { get; set; }
    public string ClubUrl { get; set; }

    [ForeignKey(nameof(BookId))]
    public int BookId { get; set; }
    public Book? Book { get; set; }

    [ForeignKey(nameof(OwnerId))]
    public string OwnerId { get; set; }
    public ApplicationUser? Owner { get; set; }

    public ICollection<ApplicationUser>? Users { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}