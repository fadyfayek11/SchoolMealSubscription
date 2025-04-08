using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMealSubscription.Models;

public class BookComments
{
    [Key]
    public int Id { get; set; }

    // Change Guid to string to match IdentityUser primary key type
    public string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }

    // Foreign key to Book
    public int BookId { get; set; }

    [ForeignKey(nameof(BookId))]
    public Book Book { get; set; }

    public string Comment { get; set; }
    public DateTime CreatedTime { get; set; }
}