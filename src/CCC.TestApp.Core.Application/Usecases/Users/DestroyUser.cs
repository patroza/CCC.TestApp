using System;
using CCC.TestApp.Core.Application.DALInterfaces;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class DestroyUserInteractor : UserInteractor, IDestroyUserRequestBoundary
    {
        public DestroyUserInteractor(IUserRepository userRepository) : base(userRepository) {}

        public void Invoke(DestroyUserRequestModel inputModel, IResponseBoundary<DestroyUserResponseModel> responder) {
            TryDestroyUser(inputModel.UserId);
            responder.Respond(CreateResponseModel());
        }

        void TryDestroyUser(Guid userId) {
            try {
                UserRepository.Destroy(userId);
            } catch (RecordDoesntExistException) {
                throw new UserDoesntExistException();
            }
        }

        static DestroyUserResponseModel CreateResponseModel() {
            return new DestroyUserResponseModel();
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