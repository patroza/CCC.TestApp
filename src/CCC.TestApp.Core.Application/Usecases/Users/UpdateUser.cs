using System;
using CCC.TestApp.Core.Application.DALInterfaces;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class UpdateUserInteractor : UserInteractor, IRequestBoundary<UpdateUserRequestModel>
    {
        readonly IResponseBoundary<UpdateUserResponseModel> _responder;

        public UpdateUserInteractor(IUserRepository userRepository, IResponseBoundary<UpdateUserResponseModel> responder)
            : base(userRepository) {
            _responder = responder;
        }

        public void Invoke(UpdateUserRequestModel inputModel) {
            var user = GetExistingUser(inputModel.UserId);
            user.UserName = inputModel.UserName;
            UserRepository.Update(user);
            _responder.Respond(new UpdateUserResponseModel());
        }
    }

    public struct UpdateUserRequestModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }

    public struct UpdateUserResponseModel {}
}