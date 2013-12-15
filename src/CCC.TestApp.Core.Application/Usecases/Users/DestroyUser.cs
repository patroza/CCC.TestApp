using System;
using CCC.TestApp.Core.Application.DALInterfaces;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class DestroyUserInteractor : UserInteractor, IDestroyUserRequestBoundary
    {
        public DestroyUserInteractor(IUserRepository userRepository) : base(userRepository) {}

        public void Invoke(DestroyUserRequestModel inputModel, IDestroyUserResponseBoundary responder) {
            var user = GetExistingUser(inputModel.UserId);
            UserRepository.Destroy(user);
            responder.Respond(new DestroyUserResponseModel());
        }
    }

    public struct DestroyUserRequestModel
    {
        public Guid UserId { get; set; }
    }

    public struct DestroyUserResponseModel {}

    public interface IDestroyUserRequestBoundary :
        IRequestBoundary<DestroyUserRequestModel, IDestroyUserResponseBoundary> {}

    public interface IDestroyUserResponseBoundary : IResponseBoundary<DestroyUserResponseModel> {}
}