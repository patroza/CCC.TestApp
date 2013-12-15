using System;
using CCC.TestApp.Core.Application.DALInterfaces;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class ShowUserInteractor : UserInteractor, IShowUserRequestBoundary
    {
        public ShowUserInteractor(IUserRepository userRepository) : base(userRepository) {}

        public void Invoke(ShowUserRequestModel inputModel, IShowUserResponseBoundary responder) {
            var user = GetExistingUser(inputModel.UserId);
            responder.Respond(new ShowUserResponseModel {UserName = user.UserName});
        }
    }

    public struct ShowUserResponseModel
    {
        public string UserName { get; set; }
    }

    public struct ShowUserRequestModel
    {
        public Guid UserId { get; set; }
    }

    public interface IShowUserResponseBoundary : IResponseBoundary<ShowUserResponseModel> {}

    public interface IShowUserRequestBoundary : IRequestBoundary<ShowUserRequestModel, IShowUserResponseBoundary> {}
}