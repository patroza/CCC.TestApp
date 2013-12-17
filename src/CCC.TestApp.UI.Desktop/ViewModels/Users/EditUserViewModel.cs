using System;
using AutoMapper;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class EditUserViewModel : ScreenBase, IResponseBoundary<UpdateUserResponseModel>,
        IResponseBoundary<ShowUserResponseModel>
    {
        readonly IShowUserRequestBoundary _showUser;
        readonly IUpdateUserRequestBoundary _updateUser;

        UserModel _user;

        public EditUserViewModel(IShowUserRequestBoundary showUser, IUpdateUserRequestBoundary updateUser) {
            _updateUser = updateUser;
            _showUser = showUser;
            base.DisplayName = "Edit User";
        }

        public UserModel User {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public void Respond(ShowUserResponseModel model) {
            User = Mapper.DynamicMap<UserModel>(model);
        }

        public void Respond(UpdateUserResponseModel model) {
            TryClose();
        }

        public void LoadUser(Guid userId) {
            _showUser.Invoke(new ShowUserRequestModel(userId), this);
        }

        public void OK() {
            _updateUser.Invoke(Mapper.DynamicMap<UpdateUserRequestModel>(User), this);
        }

        public void Cancel() {
            TryClose();
        }
    }
}