using System;
using CCC.TestApp.Core.Application.DALInterfaces;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class ShowUserInteractor : UserInteractor, IRequestBoundary<ShowUserRequestModel>
    {
        readonly IResponseBoundary<ShowUserResponseModel> _responder;

        public ShowUserInteractor(IUserRepository userRepository, IResponseBoundary<ShowUserResponseModel> responder)
            : base(userRepository) {
            _responder = responder;
        }

        public void Invoke(ShowUserRequestModel inputModel) {
            var user = GetExistingUser(inputModel.UserId);
            _responder.Respond(new ShowUserResponseModel {UserName = user.UserName, Id = user.Id});
        }
    }

    public struct ShowUserResponseModel
    {
        public string UserName { get; set; }
        public Guid Id { get; set; }
    }

    public struct ShowUserRequestModel
    {
        public ShowUserRequestModel(Guid userId) {
            UserId = userId;
        }

        public Guid UserId;
    }
}