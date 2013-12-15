using System;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;

namespace CCC.TestApp.UI.Web.Presenters
{
    public class UsersPresenter : IResponseBoundary<ChangePasswordResponseModel>, IResponseBoundary<CreateUserResponseModel>,
        IResponseBoundary<DestroyUserResponseModel>, IResponseBoundary<UpdateUserResponseModel>
    {
        public object Result;

        public void Respond(ChangePasswordResponseModel model) {
            throw new NotImplementedException();
        }

        public void Respond(CreateUserResponseModel model) {
            throw new NotImplementedException();
        }

        public void Respond(DestroyUserResponseModel model) {
            throw new NotImplementedException();
        }

        public void Respond(ShowUserResponseModel model) {
            throw new NotImplementedException();
        }

        public void Respond(UpdateUserResponseModel model) {
            throw new NotImplementedException();
        }
    }
}