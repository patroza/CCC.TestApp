using System;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class NewUserViewModel : ScreenBase, IResponseBoundary<CreateUserResponseModel>
    {
        readonly ICreateUserRequestBoundary _createUser;
        string _password;
        string _userName;

        public NewUserViewModel(ICreateUserRequestBoundary createUser) {
            _createUser = createUser;
            base.DisplayName = "New User";
        }

        public string UserName {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        public string Password {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public void Respond(CreateUserResponseModel model) {
            Clean();
            TryClose();
        }

        void Clean() {
            UserName = null;
            Password = null;
        }

        public void OK() {
            _createUser.Invoke(new CreateUserRequestModel {UserName = UserName, Password = Password}, this);
        }

        public void Cancel() {
            Clean();
            TryClose();
        }
    }
}