using System;
using CCC.TestApp.Core.Application.DALInterfaces;
using CCC.TestApp.Core.Application.Services;
using CCC.TestApp.Core.Domain.Entities;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class CreateUserInteractor : UserInteractor, CreateUserRequestBoundary
    {
        readonly Mapper _mapper;

        public CreateUserInteractor(UserRepository userRepository, Mapper mapper) : base(userRepository) {
            _mapper = mapper;
        }

        public void Invoke(CreateUserRequestModel inputModel, ResponseBoundary<CreateUserResponseModel> responder) {
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

    public interface CreateUserRequestBoundary :
        RequestBoundary<CreateUserRequestModel, ResponseBoundary<CreateUserResponseModel>> {}

    public class UserAlreadyExistsException : RecordAlreadyExistsException {}

    public struct CreateUserRequestModel : RequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public struct CreateUserResponseModel : ResponseModel
    {
        public Guid Id { get; set; }
    }
}