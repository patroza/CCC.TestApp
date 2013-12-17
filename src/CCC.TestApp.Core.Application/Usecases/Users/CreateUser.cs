using System;
using CCC.TestApp.Core.Application.DALInterfaces;
using CCC.TestApp.Core.Domain.Entities;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class CreateUserInteractor : UserInteractor, ICreateUserRequestBoundary
    {
        public CreateUserInteractor(IUserRepository userRepository) : base(userRepository) {}

        public void Invoke(CreateUserRequestModel inputModel, IResponseBoundary<CreateUserResponseModel> responder) {
            // TODO: AutoMapper?
            var newUser = new User(Guid.NewGuid()) {
                UserName = inputModel.UserName,
                Password = inputModel.Password
            };
            try {
                UserRepository.Create(newUser);
            } catch (RecordAlreadyExistsException) {
                throw new UserAlreadyExistsException();
            }
            responder.Respond(new CreateUserResponseModel {Id = newUser.Id});
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