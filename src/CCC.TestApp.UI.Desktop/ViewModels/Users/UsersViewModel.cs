using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using Caliburn.Micro;
using CCC.TestApp.Core.Application.Events;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class UsersViewModel : ScreenBase, IHandle<UserRecordDestroyed>, IHandle<UserRecordCreated>,
        IResponseBoundary<ListUsersResponseModel>
    {
        readonly UsersController _controller;
        UserModel _selectedUser;
        ObservableCollection<UserModel> _users;

        public UsersViewModel(UsersController controller) {
            _controller = controller;
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

        public void Handle(UserRecordCreated message) {
            _controller.RefreshUsers(this);
        }

        public void Handle(UserRecordDestroyed message) {
            _users.Remove(_users.First(x => x.Id == message.Id));
        }

        public void Respond(ListUsersResponseModel model) {
            Users = new ObservableCollection<UserModel>(model.Users.Select(Mapper.DynamicMap<UserModel>));
        }

        public void NewUser() {
            _controller.NewUser();
        }

        public void ShowSelectedUser() {
            var selectedUser = SelectedUser;
            if (selectedUser != null)
                ShowUser(selectedUser);
        }

        public void ShowUser(UserModel user) {
            _controller.ShowUser(user);
        }
    }
}