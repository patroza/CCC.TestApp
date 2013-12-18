using System.Collections.ObjectModel;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class UsersViewModel : ScreenBase
    {
        readonly UsersController _controller;
        UserModel _selectedUser;
        ObservableCollection<UserModel> _users;

        public UsersViewModel(ObservableCollection<UserModel> users, UsersController controller) {
            _users = users;
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