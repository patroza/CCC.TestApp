using System;
using CCC.TestApp.Core.Application.DALInterfaces;
using CCC.TestApp.Core.Application.Services;
using CCC.TestApp.Core.Domain.Entities;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class CreateUserInteractor : UserInteractor, ICreateUserRequestBoundary
    {
        readonly IMapper _mapper;

        public CreateUserInteractor(IUserRepository userRepository, IMapper mapper) : base(userRepository) {
            _mapper = mapper;
        }

        public void Invoke(CreateUserRequestModel inputModel, IResponseBoundary<CreateUserResponseModel> responder) {
            var newUser = CreateUser(inputModel);
            TryPersistUser(newUser);
            responder.Respond(CreateResponseModel(newUser));
        }

        User CreateUser(CreateUserRequestModel inputModel) {
            return _mapper.DynamicMap<User>(inputModel);
        }

        void TryPersistUser(User newUser) {
            try {
                UserRepository.Create(newUser);
            } catch (RecordAlreadyExistsException) {
                throw new UserAlreadyExistsException();
            }
        }

        static CreateUserResponseModel CreateResponseModel(User newUser) {
            return new CreateUserResponseModel {Id = newUser.Id};
        }
    }

    public interface ICreateUserRequestBoundary :
        IRequestBoundary<CreateUserRequestModel, IResponseBoundary<CreateUserResponseModel>> {}

    public class UserAlreadyExistsException : RecordAlreadyExistsException {}

    public struct CreateUserRequestModel : IRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public struct CreateUserResponseModel : IResponseModel
    {
        public Guid Id { get; set; }
    }
}