using SchoolMealSubscription.DataAccess.Data;
using SchoolMealSubscription.DataAccess.Repository.IRepository;
using SchoolMealSubscription.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMealSubscription.DataAccess.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private ApplicationDbContext _db;
        public BookRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Book obj)
        {
            _db.Books.Update(obj);
        }
    }
}
