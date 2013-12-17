using System;
using System.ComponentModel.Composition;
using AutoMapper;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Desktop.Models;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class ShowUserViewModel : ScreenBase, IResponseBoundary<ShowUserResponseModel>,
        IResponseBoundary<DestroyUserResponseModel>
    {
        readonly IDestroyUserRequestBoundary _destroyUser;
        readonly Func<Guid, ExportLifetimeContext<EditUserViewModel>> _editUserFactory;
        readonly IShowUserRequestBoundary _showUser;
        ExportLifetimeContext<EditUserViewModel> _editUserContext;
        UserModel _user;
        Guid _userId;

        public ShowUserViewModel(IShowUserRequestBoundary showUser,
            IDestroyUserRequestBoundary destroyUser,
            Func<Guid, ExportLifetimeContext<EditUserViewModel>> editUserFactory) {
            _showUser = showUser;
            _destroyUser = destroyUser;
            _editUserFactory = editUserFactory;
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
            _destroyUser.Invoke(new DestroyUserRequestModel {UserId = _userId}, this);
        }

        public void Edit() {
            _editUserContext = _editUserFactory(_userId);
            GetParentScreen().ActivateItem(_editUserContext.Value);
        }

        protected override void OnActivate() {
            _showUser.Invoke(new ShowUserRequestModel(_userId), this);
        }

        public void SetUserId(Guid userId) {
            _userId = userId;
        }
    }
}