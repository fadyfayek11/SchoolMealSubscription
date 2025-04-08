using SchoolMealSubscription.DataAccess.Data;
using SchoolMealSubscription.DataAccess.Repository.IRepository;
using SchoolMealSubscription.Models;

namespace SchoolMealSubscription.DataAccess.Repository;

public class AuthorRepository : Repository<Author>, IAuthorRepository
{
    private ApplicationDbContext _db;
    public AuthorRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    public void Update(Author obj)
    {
        _db.Authors.Update(obj);

    }
}