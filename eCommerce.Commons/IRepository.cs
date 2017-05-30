using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Commons
{
    public interface IRepository<T> : IDisposable where T : EntityBase
    {
        void Save(T entity);
        void Delete(long id);
        T GetById(long id);
        List<T> GetAll();
    }
}
