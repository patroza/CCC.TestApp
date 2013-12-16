using System;

namespace CCC.TestApp.Core.Domain.Entities
{
    public class User
    {
        public User(Guid id) {
            Id = id;
        }

        public Guid Id { get; private set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}