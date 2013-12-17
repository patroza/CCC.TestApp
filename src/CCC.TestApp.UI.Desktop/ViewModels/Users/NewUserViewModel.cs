using AutoMapper;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class NewUserViewModel : ScreenBase, IResponseBoundary<CreateUserResponseModel>
    {
        readonly ICreateUserRequestBoundary _createUser;

        public NewUserViewModel(ICreateUserRequestBoundary createUser) {
            _createUser = createUser;
            User = new UserModel();
            base.DisplayName = "New User";
        }

        public UserModel User { get; private set; }

        public void Respond(CreateUserResponseModel model) {
            TryClose();
        }

        public void OK() {
            _createUser.Invoke(Mapper.DynamicMap<CreateUserRequestModel>(User), this);
        }

        public void Cancel() {
            TryClose();
        }
    }
}