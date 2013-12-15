using System;
using CCC.TestApp.Core.Application.DALInterfaces;
using CCC.TestApp.Core.Domain.Entities;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class ChangePassword : UserInteractor, IChangePasswordRequestBoundary
    {
        public ChangePassword(IUserRepository userRepository) : base(userRepository) {}

        public void Invoke(ChangePasswordRequestModel inputModel, IChangePasswordResponseBoundary responder) {
            var user = GetExistingUser(inputModel.UserId);

            var response = new ChangePasswordResponseModel();
            if (!HandlePasswordConfirmation(inputModel, responder, response))
                return;

            if (!HandleOldPassword(inputModel, responder, user, response))
                return;

            user.Password = inputModel.Password;
            UserRepository.Update(user);
            responder.Respond(response);
        }

        static bool HandleOldPassword(ChangePasswordRequestModel inputModel, IChangePasswordResponseBoundary responder,
            User user, ChangePasswordResponseModel response) {
            if (!inputModel.OldPassword.Equals(user.Password)) {
                responder.Respond(response);
                return false;
            }
            response.OldPasswordMatched = true;
            return true;
        }

        static bool HandlePasswordConfirmation(ChangePasswordRequestModel inputModel,
            IChangePasswordResponseBoundary responder,
            ChangePasswordResponseModel response) {
            if (!inputModel.Password.Equals(inputModel.PasswordConfirmation)) {
                responder.Respond(response);
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

    public interface IChangePasswordRequestBoundary :
        IRequestBoundary<ChangePasswordRequestModel, IChangePasswordResponseBoundary> {}

    public interface IChangePasswordResponseBoundary : IResponseBoundary<ChangePasswordResponseModel> {}
}