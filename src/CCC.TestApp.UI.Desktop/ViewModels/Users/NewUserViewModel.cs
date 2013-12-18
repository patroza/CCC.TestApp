using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class NewUserViewModel : ScreenBase, IResponseBoundary<CreateUserResponseModel>
    {
        readonly UsersController _controller;

        public NewUserViewModel(UsersController controller) {
            _controller = controller;
            User = new UserModel();
            base.DisplayName = "New User";
        }

        public UserModel User { get; private set; }

        public void Respond(CreateUserResponseModel model) {
            TryClose();
            _controller.ShowUser(model.Id);
        }

        public void OK() {
            _controller.CreateUser(User, this);
        }

        public void Cancel() {
            TryClose();
        }
    }
}