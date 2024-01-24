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
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IAppUserRepository AppUser { get; private set; }


        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product= new ProductRepository(_db);
            Company = new CompanyRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            AppUser = new AppUserReository(_db);

        }

        public void Save()
        {
           _db.SaveChanges();
        }
    }
}
