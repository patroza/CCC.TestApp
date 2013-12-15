using System;
using CCC.TestApp.Core.Application.DALInterfaces;
using CCC.TestApp.Core.Domain.Entities;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public abstract class UserInteractor
    {
        protected readonly IUserRepository UserRepository;

        protected UserInteractor(IUserRepository userRepository) {
            UserRepository = userRepository;
        }

        protected User GetExistingUser(Guid userId) {
            var user = UserRepository.Find(userId);
            ConfirmUserExists(user);
            return user;
        }

        static void ConfirmUserExists(User user) {
            if (user == null)
                throw new UserDoesntExistException();
        }
    }
}