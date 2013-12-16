using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CCC.TestApp.Core.Application.DALInterfaces;
using CCC.TestApp.Core.Domain.Entities;
using CCC.TestApp.Infrastructure.DAL.Models;

namespace CCC.TestApp.Infrastructure.DAL.Repositories
{
    public class MockUserRepository : IUserRepository
    {
        readonly Dictionary<Guid, UserRow> _userList = new Dictionary<Guid, UserRow>();

        public MockUserRepository() {
            var guid = Guid.NewGuid();
            _userList.Add(guid, new UserRow {Id = guid, UserName = "Test user 1"});
            guid = Guid.NewGuid();
            _userList.Add(guid, new UserRow {Id = guid, UserName = "Test user 2"});
        }

        public User Find(Guid userId) {
            return _userList.ContainsKey(userId) ? Mapper.DynamicMap<User>(_userList[userId]) : null;
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

        public List<User> All() {
            return _userList.Values.Select(Mapper.DynamicMap<User>).ToList();
        }

        public User Find(string userName) {
            throw new NotImplementedException();
        }
    }
}