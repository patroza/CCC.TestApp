using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class EditUserViewModel : ScreenBase
    {
        readonly UsersController _controller;
        readonly UserModel _user;

        public EditUserViewModel(UserModel user, UsersController controller) {
            _user = user;
            _controller = controller;
            base.DisplayName = "Edit User";
        }

        public UserModel User {
            get { return _user; }
        }

        public void OK() {
            _controller.UpdateUser(_user);
            TryClose();
        }

        public void Cancel() {
            TryClose();
        }
    }
}