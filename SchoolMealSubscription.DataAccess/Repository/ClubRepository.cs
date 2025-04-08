using SchoolMealSubscription.DataAccess.Data;
using SchoolMealSubscription.DataAccess.Repository.IRepository;
using SchoolMealSubscription.Models;

namespace SchoolMealSubscription.DataAccess.Repository;

public class ClubRepository : Repository<Club>, IClubRepository
{
    private ApplicationDbContext _db;

    public ClubRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
}
public class UserClubRepository : Repository<UserClub>, IUserClubRepository
{
    private ApplicationDbContext _db;

    public UserClubRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
}

