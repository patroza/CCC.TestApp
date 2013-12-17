using System;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using Caliburn.Micro;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class UsersViewModel : ScreenBase, IResponseBoundary<ListUsersResponseModel>
    {
        readonly IListUsersRequestBoundary _listUsers;
        readonly Lazy<NewUserViewModel> _newUserViewModel;
        readonly Lazy<ShowUserViewModel> _userViewModel;
        UserModel _selectedUser;
        ObservableCollection<UserModel> _users;

        public UsersViewModel(IListUsersRequestBoundary listUsers,
            Lazy<ShowUserViewModel> userViewModel,
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
            if (!_userViewModel.IsValueCreated)
                _userViewModel.Value.Deactivated += ValueOnDeactivated;

            var userViewModel = _userViewModel.Value;
            userViewModel.LoadUser(user.Id);
            GetParentScreen().ActivateItem(userViewModel);
        }

        protected override void OnInitialize() {
            LoadList();
        }

        void LoadList() {
            _listUsers.Invoke(new ListUsersRequestModel(), this);
        }
    }
}