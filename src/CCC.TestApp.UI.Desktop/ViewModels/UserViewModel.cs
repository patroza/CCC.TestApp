using System;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.Core.Domain.Entities;

namespace CCC.TestApp.UI.Desktop.ViewModels
{
    public class UserViewModel : ScreenBase, IResponseBoundary<ShowUserResponseModel>
    {
        readonly Lazy<IRequestBoundary<ShowUserRequestModel>> _showUser;
        User _user;

        public UserViewModel(Lazy<IRequestBoundary<ShowUserRequestModel>> showUser) {
            _showUser = showUser;
            base.DisplayName = "Show User";
        }

        public User User {
            get { return _user; }
            private set { SetProperty(ref _user, value); }
        }

        public void Respond(ShowUserResponseModel model) {
            User = new User(model.Id) {UserName = model.UserName};
        }

        public void LoadUser(Guid userId) {
            _showUser.Value.Invoke(new ShowUserRequestModel(userId));
        }
    }
}