using System;
using CCC.TestApp.Core.Application.DALInterfaces;
using CCC.TestApp.Core.Domain.Entities;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class ChangePassword : UserInteractor, IRequestBoundary<ChangePasswordRequestModel>
    {
        readonly IResponseBoundary<ChangePasswordResponseModel> _responder;

        public ChangePassword(IUserRepository userRepository, IResponseBoundary<ChangePasswordResponseModel> responder)
            : base(userRepository) {
            _responder = responder;
        }

        public void Invoke(ChangePasswordRequestModel inputModel) {
            var user = GetExistingUser(inputModel.UserId);

            var response = new ChangePasswordResponseModel();
            ConfirmPasswordConfirmation(inputModel);
            ConfirmOldPassword(inputModel, user);

            user.Password = inputModel.Password;
            UserRepository.Update(user);
            _responder.Respond(response);
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

    public class OldPasswordMismatchException : Exception {}

    public class PasswordConfirmationMismatchException : Exception {}

    public struct ChangePasswordRequestModel
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public Guid UserId { get; set; }
    }

    public struct ChangePasswordResponseModel {}
}