using System;
using AutoMapper;
using Caliburn.Micro;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels
{
    public class ShowUserViewModel : ScreenBase, IResponseBoundary<ShowUserResponseModel>,
        IResponseBoundary<DestroyUserResponseModel>
    {
        readonly Lazy<IRequestBoundary<DestroyUserRequestModel>> _destroyUser;
        readonly Lazy<EditUserViewModel> _editUser;
        readonly Lazy<IRequestBoundary<ShowUserRequestModel>> _showUser;
        UserModel _user;

        public ShowUserViewModel(Lazy<IRequestBoundary<ShowUserRequestModel>> showUser,
            Lazy<IRequestBoundary<DestroyUserRequestModel>> destroyUser, Lazy<EditUserViewModel> editUser) {
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
            _destroyUser.Value.Invoke(new DestroyUserRequestModel {UserId = _user.Id});
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
            _showUser.Value.Invoke(new ShowUserRequestModel(userId));
        }
    }
}