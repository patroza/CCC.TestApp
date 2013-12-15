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
            if (HandlePasswordConfirmation(inputModel, response)
                && HandleOldPassword(inputModel, user, response)) {
                user.Password = inputModel.Password;
                UserRepository.Update(user);
            }
            _responder.Respond(response);
        }

        bool HandleOldPassword(ChangePasswordRequestModel inputModel, User user, ChangePasswordResponseModel response) {
            if (inputModel.OldPassword.Equals(user.Password))
                return true;
            response.Errors.Add("Old password mismatch");
            return false;
        }

        bool HandlePasswordConfirmation(ChangePasswordRequestModel inputModel, ChangePasswordResponseModel response) {
            if (inputModel.Password.Equals(inputModel.PasswordConfirmation))
                return true;
            response.Errors.Add("Password does not match confirmation");
            return false;
        }
    }

    public struct ChangePasswordRequestModel
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public Guid UserId { get; set; }
    }

    public class ChangePasswordResponseModel : ValidatingResponseModel {}
}