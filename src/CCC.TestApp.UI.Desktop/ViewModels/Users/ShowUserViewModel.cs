using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class ShowUserViewModel : ScreenBase
    {
        readonly UsersController _controller;
        readonly UserModel _user;

        public ShowUserViewModel(UserModel model, UsersController controller) {
            _user = model;
            _controller = controller;
            base.DisplayName = "Show User";
        }

        public UserModel User {
            get { return _user; }
        }

        public void Respond(DestroyUserResponseModel model) {
            TryClose();
        }

        public void Destroy() {
            _controller.DestroyUser(_user);
            TryClose();
        }

        public void Edit() {
            _controller.EditUser(_user);
        }
    }
}