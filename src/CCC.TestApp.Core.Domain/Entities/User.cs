using System;

namespace CCC.TestApp.Core.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}