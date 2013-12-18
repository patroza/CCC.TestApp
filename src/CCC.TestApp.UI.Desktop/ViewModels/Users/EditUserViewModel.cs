using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class EditUserViewModel : ScreenBase, IResponseBoundary<UpdateUserResponseModel>
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

        public void Respond(UpdateUserResponseModel model) {
            TryClose();
        }

        public void OK() {
            _controller.UpdateUser(_user, this);
        }

        public void Cancel() {
            TryClose();
        }
    }
}