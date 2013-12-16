using System;
using AutoMapper;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels
{
    public class UserViewModel : ScreenBase, IResponseBoundary<ShowUserResponseModel>
    {
        readonly Lazy<IRequestBoundary<ShowUserRequestModel>> _showUser;
        UserModel _user;

        public UserViewModel(Lazy<IRequestBoundary<ShowUserRequestModel>> showUser) {
            _showUser = showUser;
            base.DisplayName = "Show User";
        }

        public UserModel User {
            get { return _user; }
            private set { SetProperty(ref _user, value); }
        }

        public void Respond(ShowUserResponseModel model) {
            User = Mapper.DynamicMap<UserModel>(model);
        }

        public void LoadUser(Guid userId) {
            _showUser.Value.Invoke(new ShowUserRequestModel(userId));
        }
    }
}