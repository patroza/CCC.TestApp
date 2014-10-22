using System;
using CCC.TestApp.Core.Application.DALInterfaces;
using CCC.TestApp.Core.Domain.Entities;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class ChangePassword : UserInteractor, ChangePasswordRequestBoundary
    {
        public ChangePassword(UserRepository userRepository)
            : base(userRepository) {}

        public void Invoke(ChangePasswordRequestModel inputModel,
            ResponseBoundary<ChangePasswordResponseModel> responder) {
            var user = GetExistingUser(inputModel.UserId);
            Validate(inputModel, user);
            UpdateUser(inputModel, user);
            responder.Respond(CreateResponseModel());
        }

        static void Validate(ChangePasswordRequestModel inputModel, User user) {
            ValidatePasswordConfirmation(inputModel);
            ValidateOldPassword(inputModel, user);
        }

        static void ValidateOldPassword(ChangePasswordRequestModel inputModel, User user) {
            if (!inputModel.OldPassword.Equals(user.Password))
                throw new OldPasswordMismatchException();
        }

        static void ValidatePasswordConfirmation(ChangePasswordRequestModel inputModel) {
            if (!inputModel.Password.Equals(inputModel.PasswordConfirmation))
                throw new PasswordConfirmationMismatchException();
        }

        void UpdateUser(ChangePasswordRequestModel inputModel, User user) {
            user.Password = inputModel.Password;
            UserRepository.Update(user);
        }

        static ChangePasswordResponseModel CreateResponseModel() {
            return new ChangePasswordResponseModel();
        }
    }

    public interface ChangePasswordRequestBoundary :
        RequestBoundary<ChangePasswordRequestModel, ResponseBoundary<ChangePasswordResponseModel>> {}

    public class OldPasswordMismatchException : Exception {}

    public class PasswordConfirmationMismatchException : Exception {}

    public struct ChangePasswordRequestModel : RequestModel
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public Guid UserId { get; set; }
    }

    public struct ChangePasswordResponseModel : ResponseModel {}
}