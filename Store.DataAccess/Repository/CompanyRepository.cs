using Store.DataAccess.Data;
using Store.DataAccess.Repository.IRepository;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repository
{
    public class CompanyRepository:Repository<Company>,ICompanyRepository
    {
        private readonly AppDbContext _db;
        public CompanyRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
         public void Save()
        {
            _db.SaveChanges();
        }
        public void Update(Company obj)
        {
            _db.companies.Update(obj);
        }
    }
}
