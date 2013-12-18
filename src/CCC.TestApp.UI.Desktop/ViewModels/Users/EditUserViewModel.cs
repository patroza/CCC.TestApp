using CCC.TestApp.Core.Application.Services;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Controllers;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class EditUserViewModel : ScreenBase, IResponseBoundary<UpdateUserResponseModel>,
        IResponseBoundary<ShowUserResponseModel>
    {
        readonly UsersController _controller;
        readonly IMapper _mapper;
        UserModel _user;

        public EditUserViewModel(UsersController controller, IMapper mapper) {
            _controller = controller;
            _mapper = mapper;
            base.DisplayName = "Edit User";
        }

        public UserModel User {
            get { return _user; }
        }

        public void Respond(ShowUserResponseModel model) {
            _user = _mapper.DynamicMap<UserModel>(model);
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