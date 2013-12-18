using System;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    // Thoughts
    // - Both the current Test MVC and MVVM apps seem to shout: Return Interactor output directly instead of through a callback...
    // - How to notify e.g the UsersViewModel, that an item has been added, updated or removed?
    //   - e.g the DAL layer could inform us about changes, e.g through event aggregator. Or should the business layer tell us instead?
    // - By using the controller methods with UserModels instead of Guids, we easily make the mistake of directly passing and editing the existing model, which in case of validation errors is problematic
    //   - Also to revert the changes on cancelling edit, requires additional effort (e.g keeping track of the old state)
    //   - This whole point becomes no longer relevant if we solve the add/update/remove notifications..
    public class UsersController : IResponseBoundary<ShowUserResponseModel>, IResponseBoundary<ListUsersResponseModel>,
        IResponseBoundary<DestroyUserResponseModel>, IResponseBoundary<CreateUserResponseModel>,
        IResponseBoundary<UpdateUserResponseModel>
    {
        readonly ICreateUserRequestBoundary _createUser;
        readonly IDestroyUserRequestBoundary _destroyUser;
        readonly IListUsersRequestBoundary _listUsers;
        readonly ShellViewModel _shell;
        readonly IShowUserRequestBoundary _showUser;
        readonly IUpdateUserRequestBoundary _updateUser;

        public UsersController(ShellViewModel shell, IListUsersRequestBoundary listUsers,
            IShowUserRequestBoundary showUser, IDestroyUserRequestBoundary destroyUser,
            ICreateUserRequestBoundary createUser, IUpdateUserRequestBoundary updateUser) {
            _shell = shell;
            _listUsers = listUsers;
            _showUser = showUser;
            _destroyUser = destroyUser;
            _createUser = createUser;
            _updateUser = updateUser;
        }

        public void Respond(CreateUserResponseModel model) {
            ShowUser(model.Id);
        }

        public void Respond(DestroyUserResponseModel model) {}

        public void Respond(ListUsersResponseModel model) {
            _shell.ActivateItem(
                new UsersViewModel(
                    new ObservableCollection<UserModel>(model.Users.Select(Mapper.DynamicMap<UserModel>)), this));
        }

        public void Respond(ShowUserResponseModel model) {
            _shell.ActivateItem(new ShowUserViewModel(Mapper.DynamicMap<UserModel>(model), this));
        }

        public void Respond(UpdateUserResponseModel model) {}

        public void ListUsers() {
            _listUsers.Invoke(new ListUsersRequestModel(), this);
        }

        public void ShowUser(UserModel user) {
            ShowUser(user.Id);
        }

        void ShowUser(Guid userId) {
            _showUser.Invoke(new ShowUserRequestModel(userId), this);
        }

        public void DestroyUser(UserModel user) {
            _destroyUser.Invoke(new DestroyUserRequestModel {UserId = user.Id}, this);
        }

        public void NewUser() {
            _shell.ActivateItem(new NewUserViewModel(this));
        }

        public void CreateUser(UserModel user) {
            _createUser.Invoke(Mapper.DynamicMap<CreateUserRequestModel>(user), this);
        }

        public void EditUser(UserModel user) {
            _shell.ActivateItem(new EditUserViewModel(user, this));
        }

        public void UpdateUser(UserModel user) {
            _updateUser.Invoke(Mapper.DynamicMap<UpdateUserRequestModel>(user), this);
        }
    }
}