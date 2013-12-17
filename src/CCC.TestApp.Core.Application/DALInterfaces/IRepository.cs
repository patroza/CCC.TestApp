using System;
using System.Collections.Generic;

namespace CCC.TestApp.Core.Application.DALInterfaces
{
    public interface IRepository<T>
    {
        T Get(Guid id);
        void Update(T entity);
        void Destroy(Guid id);
        void Create(T entity);
        List<T> All();
    }
}