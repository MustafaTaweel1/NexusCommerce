using Store.DataAccess.Data;
using Store.DataAccess.Repository.IRepository;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repository
{
    //Repository Abstract , ICategoryRepository interface 
    //use Repository becuse dont write code for Interface again already write in Repository , declared 
    public class AppUserReository :Repository<AppUser>, IAppUserRepository
    {
        private readonly AppDbContext _db;

        public AppUserReository(AppDbContext db):base(db) {
        _db = db;
        }
    }
}
