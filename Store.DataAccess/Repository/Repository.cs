using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Data;
using System.Linq.Expressions;
using Store.DataAccess.Repository.IRepository;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Store.DataAccess.Repository
{
    public class Repository<T>: IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        // DbSet T use to Create db have value in T class  you choise 
        //
        internal DbSet<T> dbSet;
        public Repository(AppDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            _db.products.Include(w=>w.Category).Include(x=>x.CategortId);
        }
        public void Add(T entity)
        {
           
           dbSet.Add(entity);

        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
          dbSet.RemoveRange(entities);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null,bool tracked=false)
        {
            IQueryable<T> query;
            if (tracked)
            {
               query = dbSet;

            }
            else
            {
              query = dbSet.AsNoTracking();
            }



            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();

        }

            //get all use in Api 
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(filter != null)
            {
                query=query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
