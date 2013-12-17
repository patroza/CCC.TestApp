using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using AutoMapper;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class UsersViewModel : ScreenBase, IResponseBoundary<ListUsersResponseModel>
    {
        readonly IListUsersRequestBoundary _listUsers;
        readonly ExportFactory<NewUserViewModel> _newUserViewModelFactory;
        readonly Func<Guid, ExportLifetimeContext<ShowUserViewModel>> _userViewModelFactory;
        ExportLifetimeContext<NewUserViewModel> _newUserContext;
        UserModel _selectedUser;
        ExportLifetimeContext<ShowUserViewModel> _showUserContext;
        ObservableCollection<UserModel> _users;

        public UsersViewModel(IListUsersRequestBoundary listUsers,
            Func<Guid, ExportLifetimeContext<ShowUserViewModel>> userViewModelFactory,
            ExportFactory<NewUserViewModel> newUserViewModelFactory) {
            _listUsers = listUsers;
            _userViewModelFactory = userViewModelFactory;
            _newUserViewModelFactory = newUserViewModelFactory;

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

        protected override void OnActivate() {
            base.OnActivate();
            if (_showUserContext != null)
                _showUserContext.Dispose();
            if (_newUserContext != null)
                _newUserContext.Dispose();
            LoadList();
        }

        public void NewUser() {
            _newUserContext = _newUserViewModelFactory.CreateExport();
            GetParentScreen().ActivateItem(_newUserContext.Value);
        }

        public void ShowSelectedUser() {
            var selectedUser = SelectedUser;
            if (selectedUser != null)
                ShowUser(selectedUser);
        }

        public void ShowUser(UserModel user) {
            _showUserContext = _userViewModelFactory(user.Id);
            GetParentScreen().ActivateItem(_showUserContext.Value);
        }

        void LoadList() {
            _listUsers.Invoke(new ListUsersRequestModel(), this);
        }
    }
}