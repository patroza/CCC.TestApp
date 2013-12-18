using System;
using AutoMapper;
using Caliburn.Micro;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class UsersController
    {
        readonly ICreateUserRequestBoundary _createUser;
        readonly IDestroyUserRequestBoundary _destroyUser;
        readonly IEventAggregator _eventBus;
        readonly IListUsersRequestBoundary _listUsers;
        readonly ShellViewModel _shell;
        readonly IShowUserRequestBoundary _showUser;
        readonly IUpdateUserRequestBoundary _updateUser;

        public UsersController(IEventAggregator eventBus, ShellViewModel shell, IListUsersRequestBoundary listUsers,
            IShowUserRequestBoundary showUser, IDestroyUserRequestBoundary destroyUser,
            ICreateUserRequestBoundary createUser, IUpdateUserRequestBoundary updateUser) {
            _eventBus = eventBus;
            _shell = shell;
            _listUsers = listUsers;
            _showUser = showUser;
            _destroyUser = destroyUser;
            _createUser = createUser;
            _updateUser = updateUser;
        }

        public void ListUsers() {
            var vm = new UsersViewModel(this);
            _listUsers.Invoke(new ListUsersRequestModel(), vm);
            _eventBus.Subscribe(vm);
            _shell.ActivateItem(vm);
        }

        public void ShowUser(UserModel user) {
            ShowUser(user.Id);
        }

        public void ShowUser(Guid userId) {
            var vm = new ShowUserViewModel(this);
            _showUser.Invoke(new ShowUserRequestModel(userId), vm);
            _shell.ActivateItem(vm);
        }

        public void DestroyUser(UserModel user, IResponseBoundary<DestroyUserResponseModel> responder) {
            _destroyUser.Invoke(new DestroyUserRequestModel {UserId = user.Id}, responder);
        }

        public void NewUser() {
            _shell.ActivateItem(new NewUserViewModel(this));
        }

        public void CreateUser(UserModel user, IResponseBoundary<CreateUserResponseModel> responder) {
            _createUser.Invoke(Mapper.DynamicMap<CreateUserRequestModel>(user), responder);
        }

        public void EditUser(UserModel user) {
            _shell.ActivateItem(new EditUserViewModel(user, this));
        }

        public void UpdateUser(UserModel user, IResponseBoundary<UpdateUserResponseModel> responder) {
            _updateUser.Invoke(Mapper.DynamicMap<UpdateUserRequestModel>(user), responder);
        }

        public void RefreshUsers(IResponseBoundary<ListUsersResponseModel> responder) {
            _listUsers.Invoke(new ListUsersRequestModel(), responder);
        }
    }
}