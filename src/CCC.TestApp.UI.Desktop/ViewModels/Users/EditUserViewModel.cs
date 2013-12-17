using System;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class EditUserViewModel : ScreenBase, IResponseBoundary<UpdateUserResponseModel>,
        IResponseBoundary<ShowUserResponseModel>
    {
        readonly IShowUserRequestBoundary _showUser;
        readonly IUpdateUserRequestBoundary _updateUser;
        string _password;
        Guid _userId;
        string _userName;

        public EditUserViewModel(IShowUserRequestBoundary showUser, IUpdateUserRequestBoundary updateUser) {
            _updateUser = updateUser;
            _showUser = showUser;
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

        public void Respond(ShowUserResponseModel model) {
            UserName = model.UserName;
            Password = model.Password;
        }

        public void Respond(UpdateUserResponseModel model) {
            Clear();
            TryClose();
        }

        public void LoadUser(Guid userId) {
            _userId = userId;
            _showUser.Invoke(new ShowUserRequestModel(userId), this);
        }

        public void OK() {
            _updateUser.Invoke(new UpdateUserRequestModel {
                UserId = _userId,
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