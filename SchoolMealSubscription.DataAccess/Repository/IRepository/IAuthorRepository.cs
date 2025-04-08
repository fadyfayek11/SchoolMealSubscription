using SchoolMealSubscription.Models;

namespace SchoolMealSubscription.DataAccess.Repository.IRepository;

public interface IAuthorRepository : IRepository<Author>
{
    void Update(Author obj);
}