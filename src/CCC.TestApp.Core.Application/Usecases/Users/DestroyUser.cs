using System;
using CCC.TestApp.Core.Application.DALInterfaces;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class DestroyUserInteractor : UserInteractor, IRequestBoundary<DestroyUserRequestModel>
    {
        readonly IResponseBoundary<DestroyUserResponseModel> _responder;

        public DestroyUserInteractor(IUserRepository userRepository,
            IResponseBoundary<DestroyUserResponseModel> responder)
            : base(userRepository) {
            _responder = responder;
        }

        public void Invoke(DestroyUserRequestModel inputModel) {
            var user = GetExistingUser(inputModel.UserId);
            try {
                UserRepository.Destroy(user);
            } catch (RecordDoesntExistException) {
                throw new UserDoesntExistException();
            }
            _responder.Respond(new DestroyUserResponseModel());
        }
    }

    public struct DestroyUserRequestModel
    {
        public Guid UserId { get; set; }
    }

    public struct DestroyUserResponseModel {}
}