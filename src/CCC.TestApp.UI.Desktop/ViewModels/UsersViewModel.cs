using System;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.Core.Domain.Entities;

namespace CCC.TestApp.UI.Desktop.ViewModels
{
    public class UsersViewModel : ScreenBase, IResponseBoundary<ListUsersResponseModel>
    {
        readonly Lazy<IRequestBoundary<ListUsersRequestModel>> _listUsers;
        readonly Lazy<UserViewModel> _userViewModel;
        User _selectedUser;
        ObservableCollection<User> _users;

        public UsersViewModel(Lazy<IRequestBoundary<ListUsersRequestModel>> listUsers, Lazy<UserViewModel> userViewModel) {
            _listUsers = listUsers;
            _userViewModel = userViewModel;

            base.DisplayName = "Users";
        }

        public ObservableCollection<User> Users {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }

        public User SelectedUser {
            get { return _selectedUser; }
            set { SetProperty(ref _selectedUser, value); }
        }

        public void Respond(ListUsersResponseModel model) {
            Users = new ObservableCollection<User>(model.Users);
        }

        IConductor GetParentScreen() {
            return (IConductor) Parent;
        }

        public void ShowSelectedUser() {
            var selectedUser = SelectedUser;
            if (selectedUser != null)
                ShowUser(selectedUser);
        }

        public void ShowUser(User user) {
            var userViewModel = _userViewModel.Value;
            userViewModel.LoadUser(user.Id);
            GetParentScreen().ActivateItem(userViewModel);
        }

        protected override void OnInitialize() {
            _listUsers.Value.Invoke(new ListUsersRequestModel());
        }
    }
}