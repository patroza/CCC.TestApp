using System;
using CCC.TestApp.Core.Application.Services;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Models;
using CCC.TestApp.UI.Desktop.ViewModels;
using CCC.TestApp.UI.Desktop.ViewModels.Users;

namespace CCC.TestApp.UI.Desktop.Controllers
{
    public class UsersController
    {
        readonly CreateUserRequestBoundary _createUser;
        readonly DestroyUserRequestBoundary _destroyUser;
        readonly EventBus _eventBus;
        readonly ListUsersRequestBoundary _listUsers;
        readonly Mapper _mapper;
        readonly ShellViewModel _shell;
        readonly ShowUserRequestBoundary _showUser;
        readonly UpdateUserRequestBoundary _updateUser;

        public UsersController(EventBus eventBus, Mapper mapper, ShellViewModel shell,
            ListUsersRequestBoundary listUsers,
            ShowUserRequestBoundary showUser, DestroyUserRequestBoundary destroyUser,
            CreateUserRequestBoundary createUser, UpdateUserRequestBoundary updateUser) {
            _eventBus = eventBus;
            _mapper = mapper;
            _shell = shell;
            _listUsers = listUsers;
            _showUser = showUser;
            _destroyUser = destroyUser;
            _createUser = createUser;
            _updateUser = updateUser;
        }

        public void ListUsers() {
            var vm = new UsersViewModel(this, _mapper);
            _listUsers.Invoke(new ListUsersRequestModel(), vm);
            _eventBus.Subscribe(vm);
            _shell.ActivateItem(vm);
        }

        public void ShowUser(Guid userId) {
            var vm = new ShowUserViewModel(this, _mapper);
            _eventBus.Subscribe(vm);
            _showUser.Invoke(new ShowUserRequestModel(userId), vm);
            _shell.ActivateItem(vm);
        }

        public void DestroyUser(Guid userId, ResponseBoundary<DestroyUserResponseModel> responder) {
            _destroyUser.Invoke(new DestroyUserRequestModel {UserId = userId}, responder);
        }

        public void NewUser() {
            _shell.ActivateItem(new NewUserViewModel(this));
        }

        public void CreateUser(UserModel user, ResponseBoundary<CreateUserResponseModel> responder) {
            _createUser.Invoke(_mapper.DynamicMap<CreateUserRequestModel>(user), responder);
        }

        public void EditUser(Guid userId) {
            var vm = new EditUserViewModel(this, _mapper);
            _showUser.Invoke(new ShowUserRequestModel(userId), vm);
            _shell.ActivateItem(vm);
        }

        public void UpdateUser(UserModel user, ResponseBoundary<UpdateUserResponseModel> responder) {
            _updateUser.Invoke(_mapper.DynamicMap<UpdateUserRequestModel>(user), responder);
        }

        public void RefreshUsers(ResponseBoundary<ListUsersResponseModel> responder) {
            _listUsers.Invoke(new ListUsersRequestModel(), responder);
        }

        public void RefreshUser(Guid id, ResponseBoundary<ShowUserResponseModel> responder) {
            _showUser.Invoke(new ShowUserRequestModel(id), responder);
        }
    }
}