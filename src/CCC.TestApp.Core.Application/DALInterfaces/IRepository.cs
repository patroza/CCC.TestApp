using System;
using System.Collections.Generic;
using CCC.TestApp.Core.Domain.Entities;

namespace CCC.TestApp.Core.Application.DALInterfaces
{
    public interface IRepository<T>
    {
        T Find(Guid id);
        void Update(T entity);
        void Destroy(Guid id);
        void Create(T entity);
        List<User> All();
    }
}