using System;
using CCC.TestApp.Core.Application.DALInterfaces;
using CCC.TestApp.Core.Domain.Entities;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class ChangePassword : UserInteractor, IChangePasswordRequestBoundary
    {
        public ChangePassword(IUserRepository userRepository)
            : base(userRepository) {}

        public void Invoke(ChangePasswordRequestModel inputModel,
            IResponseBoundary<ChangePasswordResponseModel> responder) {
            var user = GetExistingUser(inputModel.UserId);

            var response = new ChangePasswordResponseModel();
            ConfirmPasswordConfirmation(inputModel);
            ConfirmOldPassword(inputModel, user);

            user.Password = inputModel.Password;
            UserRepository.Update(user);
            responder.Respond(response);
        }

        static void ConfirmOldPassword(ChangePasswordRequestModel inputModel, User user) {
            if (!inputModel.OldPassword.Equals(user.Password))
                throw new OldPasswordMismatchException();
        }

        static void ConfirmPasswordConfirmation(ChangePasswordRequestModel inputModel) {
            if (!inputModel.Password.Equals(inputModel.PasswordConfirmation))
                throw new PasswordConfirmationMismatchException();
        }
    }

    public interface IChangePasswordRequestBoundary :
        IRequestBoundary<ChangePasswordRequestModel, IResponseBoundary<ChangePasswordResponseModel>> {}

    public class OldPasswordMismatchException : Exception {}

    public class PasswordConfirmationMismatchException : Exception {}

    public struct ChangePasswordRequestModel : IRequestModel
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public Guid UserId { get; set; }
    }

    public struct ChangePasswordResponseModel : IResponseModel {}
}