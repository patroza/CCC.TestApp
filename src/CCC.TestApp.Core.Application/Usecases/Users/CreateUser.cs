using System;
using CCC.TestApp.Core.Application.DALInterfaces;
using CCC.TestApp.Core.Domain.Entities;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class CreateUserInteractor : UserInteractor, IRequestBoundary<CreateUserRequestModel>
    {
        readonly IResponseBoundary<CreateUserResponseModel> _responder;

        public CreateUserInteractor(IUserRepository userRepository, IResponseBoundary<CreateUserResponseModel> responder)
            : base(userRepository) {
            _responder = responder;
        }

        public void Invoke(CreateUserRequestModel inputModel) {
            try {
                UserRepository.Create(new User(Guid.NewGuid()));
            } catch (RecordAlreadyExistsException) {
                throw new UserAlreadyExistsException();
            }
            _responder.Respond(new CreateUserResponseModel());
        }
    }

    public class UserAlreadyExistsException : RecordAlreadyExistsException {}

    public struct CreateUserRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public struct CreateUserResponseModel
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
    }
}