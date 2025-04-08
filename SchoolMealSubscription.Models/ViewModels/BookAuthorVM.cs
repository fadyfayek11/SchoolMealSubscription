namespace SchoolMealSubscription.Models.ViewModels;

public class BookAuthorVM
{
    public IEnumerable<Book> Books { get; set; }
    public IEnumerable<Author> Authors { get; set; }
}