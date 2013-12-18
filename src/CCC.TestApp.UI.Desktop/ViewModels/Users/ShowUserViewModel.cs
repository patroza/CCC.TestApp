using AutoMapper;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class ShowUserViewModel : ScreenBase, IResponseBoundary<ShowUserResponseModel>,
        IResponseBoundary<DestroyUserResponseModel>
    {
        readonly UsersController _controller;
        UserModel _user;

        public ShowUserViewModel(UsersController controller) {
            _controller = controller;
            base.DisplayName = "Show User";
        }

        public UserModel User {
            get { return _user; }
        }

        public void Respond(DestroyUserResponseModel model) {
            TryClose();
        }

        public void Respond(ShowUserResponseModel model) {
            _user = Mapper.DynamicMap<UserModel>(model);
        }

        public void Destroy() {
            _controller.DestroyUser(_user, this);
        }

        public void Edit() {
            _controller.EditUser(_user);
        }
    }
}