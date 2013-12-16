using System;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using Caliburn.Micro;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels
{
    public class UsersViewModel : ScreenBase, IResponseBoundary<ListUsersResponseModel>
    {
        readonly Lazy<IRequestBoundary<ListUsersRequestModel>> _listUsers;
        readonly Lazy<NewUserViewModel> _newUserViewModel;
        readonly Lazy<UserViewModel> _userViewModel;
        UserModel _selectedUser;
        ObservableCollection<UserModel> _users;

        public UsersViewModel(Lazy<IRequestBoundary<ListUsersRequestModel>> listUsers, Lazy<UserViewModel> userViewModel,
            Lazy<NewUserViewModel> newUserViewModel) {
            _listUsers = listUsers;
            _userViewModel = userViewModel;
            _newUserViewModel = newUserViewModel;

            base.DisplayName = "Users";
        }

        public ObservableCollection<UserModel> Users {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }

        public UserModel SelectedUser {
            get { return _selectedUser; }
            set { SetProperty(ref _selectedUser, value); }
        }

        public void Respond(ListUsersResponseModel model) {
            Users = new ObservableCollection<UserModel>(model.Users.Select(Mapper.DynamicMap<UserModel>));
        }

        IConductor GetParentScreen() {
            return (IConductor) Parent;
        }

        public void NewUser() {
            if (!_newUserViewModel.IsValueCreated)
                _newUserViewModel.Value.Deactivated += ValueOnDeactivated;
            GetParentScreen().ActivateItem(_newUserViewModel.Value);
        }

        void ValueOnDeactivated(object sender, DeactivationEventArgs deactivationEventArgs) {
            LoadList();
        }

        public void ShowSelectedUser() {
            var selectedUser = SelectedUser;
            if (selectedUser != null)
                ShowUser(selectedUser);
        }

        public void ShowUser(UserModel user) {
            var userViewModel = _userViewModel.Value;
            userViewModel.LoadUser(user.Id);
            GetParentScreen().ActivateItem(userViewModel);
        }

        protected override void OnInitialize() {
            LoadList();
        }

        void LoadList() {
            _listUsers.Value.Invoke(new ListUsersRequestModel());
        }
    }

    public class NewUserViewModel : ScreenBase, IResponseBoundary<CreateUserResponseModel>
    {
        readonly Lazy<IRequestBoundary<CreateUserRequestModel>> _createUser;
        string _password;
        string _userName;

        public NewUserViewModel(Lazy<IRequestBoundary<CreateUserRequestModel>> createUser) {
            _createUser = createUser;
        }

        public string UserName {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        public string Password {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public void Respond(CreateUserResponseModel model) {
            TryClose();
        }

        public void OK() {
            _createUser.Value.Invoke(new CreateUserRequestModel {UserName = UserName, Password = Password});
        }

        public void Cancel() {
            TryClose();
        }
    }
}