using System;
using System.Collections.Generic;

namespace CCC.TestApp.Core.Application.DALInterfaces
{
    public interface Repository<T>
    {
        T Get(Guid id);
        void Update(T entity);
        void Destroy(Guid id);
        void Create(T entity);
        IEnumerable<T> All();
    }
}