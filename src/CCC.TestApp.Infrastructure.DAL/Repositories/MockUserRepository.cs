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
            lock (_userList)
                return _userList.ContainsKey(userId) ? Mapper.DynamicMap<User>(_userList[userId]) : null;
        }

        public void Update(User user) {
            throw new NotImplementedException();
        }

        public void Destroy(User user) {
            throw new NotImplementedException();
        }

        public void Create(User user) {
            lock (_userList) {
                if (_userList.Values.Any(x => x.UserName.Equals(user.UserName)))
                    throw new RecordAlreadyExistsException();

                var userRow = Mapper.DynamicMap<UserRow>(user);
                userRow.Id = Guid.NewGuid();
                _userList.Add(userRow.Id, Mapper.DynamicMap<UserRow>(user));
            }
        }

        public List<User> All() {
            lock (_userList)
                return _userList.Values.Select(Mapper.DynamicMap<User>).ToList();
        }
    }
}