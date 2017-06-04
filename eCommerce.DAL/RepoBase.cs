using eCommerce.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL
{
    public class RepoBase<T> : IRepository<T> where T : EntityBase
    {
        protected CommerceContext context;
        protected DbSet<T> dbSet;

        public RepoBase(CommerceContext _context)
        {
            context = _context;
            dbSet = context.Set<T>();
        }

        public void Delete(long id)
        {
            var entity = GetById(id);
            dbSet.Remove(entity);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public List<T> GetAll()
        {
            return dbSet.ToListAsync().Result;
        }

        public T GetById(long id)
        {
            return dbSet.Find(id);
        }

        public void Save(T entity)
        {
            if (entity.Id == 0)
            {
                dbSet.Add(entity);
            }
            else
            {
                context.Entry(entity).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
    }
}
