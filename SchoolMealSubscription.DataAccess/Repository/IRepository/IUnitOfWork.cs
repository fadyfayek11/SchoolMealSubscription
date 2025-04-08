using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMealSubscription.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IBookRepository Book { get; }
        IAuthorRepository Author { get; }
        IClubRepository Club { get; }
        IUserClubRepository UserClub { get; }
        ICommentsRepository Comments { get; }
        IApplicationUserRepository ApplicationUser { get; }

        void Save();
    }
}
