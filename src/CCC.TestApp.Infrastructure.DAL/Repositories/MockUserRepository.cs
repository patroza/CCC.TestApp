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

        public User Get(Guid userId) {
            lock (_userList)
                return _userList.ContainsKey(userId) ? Mapper.DynamicMap<User>(_userList[userId]) : null;
        }

        public void Update(User user) {
            lock (_userList) {
                ValidateExists(user.Id);
                UpdateExisting(user);
            }
        }

        public void Destroy(Guid userId) {
            lock (_userList) {
                ValidateExists(userId);
                DestroyRecord(userId);
            }
        }

        public void Create(User user) {
            lock (_userList) {
                ValidateDoesntExistYet(user.UserName);
                var userRow = CreateUserRow(user);
                AddToStorage(userRow);
                user.Id = userRow.Id;
            }
        }

        public List<User> All() {
            lock (_userList)
                return _userList.Values.Select(Mapper.DynamicMap<User>).ToList();
        }

        void UpdateExisting(User user) {
            var existingRecord = _userList[user.Id];
            Mapper.DynamicMap(user, existingRecord);
        }

        void ValidateExists(Guid userId) {
            if (!_userList.ContainsKey(userId))
                throw new RecordDoesntExistException();
        }

        void DestroyRecord(Guid userId) {
            var record = _userList[userId];
            _userList.Remove(userId);
            record.Id = new Guid();
        }

        void AddToStorage(UserRow userRow) {
            userRow.Id = Guid.NewGuid();
            ValidateDoesntExistYet(userRow.Id);
            _userList.Add(userRow.Id, userRow);
        }

        static UserRow CreateUserRow(User user) {
            return Mapper.DynamicMap<UserRow>(user);
        }

        void ValidateDoesntExistYet(string userName) {
            if (_userList.Values.Any(x => x.UserName.Equals(userName)))
                throw new RecordAlreadyExistsException();
        }

        void ValidateDoesntExistYet(Guid userId) {
            if (_userList.ContainsKey(userId))
                throw new RecordAlreadyExistsException();
        }
    }
}