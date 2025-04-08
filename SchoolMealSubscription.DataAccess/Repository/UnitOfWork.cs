using SchoolMealSubscription.DataAccess.Data;
using SchoolMealSubscription.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMealSubscription.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _dbContext;
        public ICategoryRepository Category { get; private set; }
        public IBookRepository Book { get; private set; }
        public IAuthorRepository Author { get; private set; }
        public IClubRepository Club { get; private set; }
        public IUserClubRepository UserClub { get; private set; }
        public ICommentsRepository Comments { get; private set; }


        public IApplicationUserRepository ApplicationUser { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            this._dbContext = db;
            Club = new ClubRepository(db);
            UserClub = new UserClubRepository(db);
            Comments = new CommentsRepository(db);
            Category = new CategoryRepository(db);
            Author = new AuthorRepository(db);
            Book = new BookRepository(db);
            ApplicationUser = new ApplicationUserRepository(db);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
