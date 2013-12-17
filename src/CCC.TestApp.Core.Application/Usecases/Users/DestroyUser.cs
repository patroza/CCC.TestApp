using System;
using CCC.TestApp.Core.Application.DALInterfaces;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class DestroyUserInteractor : UserInteractor, IDestroyUserRequestBoundary
    {
        public DestroyUserInteractor(IUserRepository userRepository) : base(userRepository) {}

        public void Invoke(DestroyUserRequestModel inputModel, IResponseBoundary<DestroyUserResponseModel> responder) {
            var user = GetExistingUser(inputModel.UserId);
            try {
                UserRepository.Destroy(user);
            } catch (RecordDoesntExistException) {
                throw new UserDoesntExistException();
            }
            responder.Respond(new DestroyUserResponseModel());
        }
    }

    public interface IDestroyUserRequestBoundary :
        IRequestBoundary<DestroyUserRequestModel, IResponseBoundary<DestroyUserResponseModel>> {}

    public struct DestroyUserRequestModel : IRequestModel
    {
        public Guid UserId { get; set; }
    }

    public struct DestroyUserResponseModel : IResponseModel {}
}