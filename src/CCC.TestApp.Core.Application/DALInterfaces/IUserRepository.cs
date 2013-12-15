using System;
using System.Collections.Generic;
using CCC.TestApp.Core.Domain.Entities;

namespace CCC.TestApp.Core.Application.DALInterfaces
{
    public interface IUserRepository
    {
        User Find(Guid userId);
        void Update(User user);
        void Destroy(User user);
        void Create(User user);
        List<User> All();
    }
}