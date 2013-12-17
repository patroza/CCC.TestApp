using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class EditUserViewModel : ScreenBase, IResponseBoundary<UpdateUserResponseModel>
    {
        readonly IUpdateUserRequestBoundary _updateUser;
        string _password;
        UserModel _user;
        string _userName;

        public EditUserViewModel(IUpdateUserRequestBoundary updateUser) {
            _updateUser = updateUser;
            base.DisplayName = "Edit User";
        }

        public string UserName {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        public string Password {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public UserModel User {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public void Respond(UpdateUserResponseModel model) {
            Clear();
            TryClose();
        }

        public void LoadUser(UserModel user) {
            User = user;
            UserName = user.UserName;
            Password = user.Password;
        }

        public void OK() {
            _updateUser.Invoke(new UpdateUserRequestModel {
                UserId = _user.Id,
                UserName = UserName,
                Password = Password
            }, this);
        }

        public void Cancel() {
            Clear();
            TryClose();
        }

        void Clear() {
            UserName = null;
            Password = null;
        }
    }
}