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
    public class ShoppingCartRepository : Repository<ShoppingCart>,IShoppingCartRepository
    {
        private readonly AppDbContext _db;

        public ShoppingCartRepository(AppDbContext db):base(db) 
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public void Update(ShoppingCart obj)
        {
            _db.ShoppingCarts.Update(obj);
        }
    }
}
