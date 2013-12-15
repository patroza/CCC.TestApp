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
            if (!HandlePasswordConfirmation(inputModel, response))
                return;

            if (!HandleOldPassword(inputModel, user, response))
                return;

            user.Password = inputModel.Password;
            UserRepository.Update(user);
            _responder.Respond(response);
        }

        bool HandleOldPassword(ChangePasswordRequestModel inputModel, User user, ChangePasswordResponseModel response) {
            if (!inputModel.OldPassword.Equals(user.Password)) {
                _responder.Respond(response);
                return false;
            }
            response.OldPasswordMatched = true;
            return true;
        }

        bool HandlePasswordConfirmation(ChangePasswordRequestModel inputModel, ChangePasswordResponseModel response) {
            if (!inputModel.Password.Equals(inputModel.PasswordConfirmation)) {
                _responder.Respond(response);
                return false;
            }
            response.PasswordsMatched = true;
            return true;
        }
    }

    public struct ChangePasswordRequestModel
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public Guid UserId { get; set; }
    }

    public struct ChangePasswordResponseModel
    {
        public bool PasswordsMatched { get; set; }
        public bool OldPasswordMatched { get; set; }
    }
}