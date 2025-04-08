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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ApplicationUser obj)
        {
            _db.Users.Update(obj);
        }
    }
   
   
}
