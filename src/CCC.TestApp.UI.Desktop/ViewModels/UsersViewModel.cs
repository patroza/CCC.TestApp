using System;
using System.Collections.Generic;
using Caliburn.Micro;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.Core.Domain.Entities;

namespace CCC.TestApp.UI.Desktop.ViewModels
{
    public class UsersViewModel : Screen, IResponseBoundary<ListUsersResponseModel>
    {
        readonly Lazy<IRequestBoundary<ListUsersRequestModel>> _listUsers;
        List<User> _users;

        public UsersViewModel(Lazy<IRequestBoundary<ListUsersRequestModel>> listUsers) {
            _listUsers = listUsers;

            base.DisplayName = "Users";
        }

        public List<User> Users {
            get { return _users; }
            set {
                _users = value;
                NotifyOfPropertyChange();
            }
        }

        public void Respond(ListUsersResponseModel model) {
            Users = model.Users;
        }

        protected override void OnInitialize() {
            _listUsers.Value.Invoke(new ListUsersRequestModel());
        }
    }
}