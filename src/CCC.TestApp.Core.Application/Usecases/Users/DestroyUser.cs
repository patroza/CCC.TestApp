using System;
using CCC.TestApp.Core.Application.DALInterfaces;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class DestroyUserInteractor : UserInteractor, DestroyUserRequestBoundary
    {
        public DestroyUserInteractor(UserRepository userRepository) : base(userRepository) {}

        public void Invoke(DestroyUserRequestModel inputModel, ResponseBoundary<DestroyUserResponseModel> responder) {
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

    public interface DestroyUserRequestBoundary :
        RequestBoundary<DestroyUserRequestModel, ResponseBoundary<DestroyUserResponseModel>> {}

    public struct DestroyUserRequestModel : RequestModel
    {
        public Guid UserId { get; set; }
    }

    public struct DestroyUserResponseModel : ResponseModel {}
}