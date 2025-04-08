using SchoolMealSubscription.DataAccess.Data;
using SchoolMealSubscription.DataAccess.Repository.IRepository;
using SchoolMealSubscription.Models;

namespace SchoolMealSubscription.DataAccess.Repository;

public class CommentsRepository : Repository<BookComments>, ICommentsRepository
{
    private ApplicationDbContext _db;

    public CommentsRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
}