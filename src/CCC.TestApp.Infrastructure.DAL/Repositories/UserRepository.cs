using System;
using CCC.TestApp.Core.Application.DALInterfaces;
using CCC.TestApp.Core.Domain.Entities;

namespace CCC.TestApp.Infrastructure.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        public User Find(Guid userId) {
            throw new NotImplementedException();
        }

        public User Find(string userName) {
            throw new NotImplementedException();
        }

        public void Update(User user) {
            throw new NotImplementedException();
        }

        public void Destroy(User user) {
            throw new NotImplementedException();
        }

        public void Create(User user) {
            if (Find(user.UserName) != null)
                throw new RecordAlreadyExistsException();
        }
    }
}