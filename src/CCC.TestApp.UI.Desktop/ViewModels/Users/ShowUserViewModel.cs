using System;
using AutoMapper;
using Caliburn.Micro;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class ShowUserViewModel : ScreenBase, IResponseBoundary<ShowUserResponseModel>,
        IResponseBoundary<DestroyUserResponseModel>
    {
        readonly IDestroyUserRequestBoundary _destroyUser;
        readonly Lazy<EditUserViewModel> _editUser;
        readonly IShowUserRequestBoundary _showUser;
        UserModel _user;

        public ShowUserViewModel(IShowUserRequestBoundary showUser,
            IDestroyUserRequestBoundary destroyUser, Lazy<EditUserViewModel> editUser) {
            _showUser = showUser;
            _destroyUser = destroyUser;
            _editUser = editUser;
            base.DisplayName = "Show User";
        }

        public UserModel User {
            get { return _user; }
            private set { SetProperty(ref _user, value); }
        }

        public void Respond(DestroyUserResponseModel model) {
            TryClose();
        }

        public void Respond(ShowUserResponseModel model) {
            User = Mapper.DynamicMap<UserModel>(model);
        }

        public void Destroy() {
            _destroyUser.Invoke(new DestroyUserRequestModel {UserId = _user.Id}, this);
        }

        public void Edit() {
            if (!_editUser.IsValueCreated)
                _editUser.Value.Deactivated += ValueOnDeactivated;

            _editUser.Value.LoadUser(_user);
            GetParentScreen().ActivateItem(_editUser.Value);
        }

        void ValueOnDeactivated(object sender, DeactivationEventArgs deactivationEventArgs) {
            LoadUser(_user.Id);
        }

        public void LoadUser(Guid userId) {
            _showUser.Invoke(new ShowUserRequestModel(userId), this);
        }
    }
}