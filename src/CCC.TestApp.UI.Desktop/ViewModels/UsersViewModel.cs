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
        User _selectedUser;
        ObservableCollection<User> _users;

        public UsersViewModel(Lazy<IRequestBoundary<ListUsersRequestModel>> listUsers) {
            _listUsers = listUsers;

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
            ShowUser(SelectedUser);
        }

        public void ShowUser(User user) {
            GetParentScreen().ActivateItem(new UserViewModel(user));
        }

        protected override void OnInitialize() {
            _listUsers.Value.Invoke(new ListUsersRequestModel());
        }
    }
}