using eCommerce.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DAL
{
    public class RepoBase<T> : IRepository<T> where T : EntityBase
    {
        protected DbContextOptionsBuilder options = new DbContextOptionsBuilder();
        protected IConfigurationRoot Configuration { get; }
        protected CommerceContext context;
        protected DbSet<T> dbSet;

        public RepoBase()
        {
            context = new CommerceContext(options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options);
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
            return dbSet.OrderBy(o => o.Id).ToListAsync().Result;
        }

        public T GetById(long id)
        {
            return dbSet.Find(id);
        }

        public void Save(T entity)
        {
            if (entity.Id == 0)
                dbSet.Add(entity);
            else
            {
                context.Entry(entity).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
    }
}
