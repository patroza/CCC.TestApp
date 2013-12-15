using System;
using CCC.TestApp.Core.Application.DALInterfaces;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class UpdateUserInteractor : UserInteractor, IUpdateUserRequestBoundary
    {
        public UpdateUserInteractor(IUserRepository userRepository) : base(userRepository) {}

        public void Invoke(UpdateUserRequestModel inputModel, IUpdateUserResponseBoundary responder) {
            var user = GetExistingUser(inputModel.UserId);
            user.UserName = inputModel.UserName;
            UserRepository.Update(user);
            responder.Respond(new UpdateUserResponseModel());
        }
    }

    public struct UpdateUserRequestModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }

    public struct UpdateUserResponseModel {}

    public interface IUpdateUserResponseBoundary : IResponseBoundary<UpdateUserResponseModel> {}

    public interface IUpdateUserRequestBoundary : IRequestBoundary<UpdateUserRequestModel, IUpdateUserResponseBoundary> {}
}