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
        readonly Lazy<UserViewModel> _userViewModel;
        UserModel _selectedUser;
        ObservableCollection<UserModel> _users;

        public UsersViewModel(Lazy<IRequestBoundary<ListUsersRequestModel>> listUsers, Lazy<UserViewModel> userViewModel) {
            _listUsers = listUsers;
            _userViewModel = userViewModel;

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
            _listUsers.Value.Invoke(new ListUsersRequestModel());
        }
    }
}