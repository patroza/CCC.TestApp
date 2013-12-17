﻿using System;
using System.ComponentModel.Composition;
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
        readonly ExportFactory<EditUserViewModel> _editUser;
        readonly IShowUserRequestBoundary _showUser;
        ExportLifetimeContext<EditUserViewModel> _editUserContext;
        UserModel _user;

        public ShowUserViewModel(IShowUserRequestBoundary showUser,
            IDestroyUserRequestBoundary destroyUser, ExportFactory<EditUserViewModel> editUser) {
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
            _editUserContext = _editUser.CreateExport();
            _editUserContext.Value.Deactivated += EditUserDeactivated;
            _editUserContext.Value.LoadUser(_user);
            GetParentScreen().ActivateItem(_editUserContext.Value);
        }

        void EditUserDeactivated(object sender, DeactivationEventArgs deactivationEventArgs) {
            _editUserContext.Value.Deactivated -= EditUserDeactivated;
            _editUserContext.Dispose();
            LoadUser(_user.Id);
        }

        public void LoadUser(Guid userId) {
            _showUser.Invoke(new ShowUserRequestModel(userId), this);
        }
    }
}