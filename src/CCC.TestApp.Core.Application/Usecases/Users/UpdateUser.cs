using System;
using CCC.TestApp.Core.Application.DALInterfaces;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class UpdateUserInteractor : UserInteractor, IUpdateUserRequestBoundary
    {
        public UpdateUserInteractor(IUserRepository userRepository) : base(userRepository) {}

        public void Invoke(UpdateUserRequestModel inputModel, IResponseBoundary<UpdateUserResponseModel> responder) {
            UpdateUser(inputModel);
            responder.Respond(CreateResponseModel());
        }

        void UpdateUser(UpdateUserRequestModel inputModel) {
            var user = GetExistingUser(inputModel.UserId);
            user.UserName = inputModel.UserName;
            user.Password = inputModel.Password;
            UserRepository.Update(user);
        }

        static UpdateUserResponseModel CreateResponseModel() {
            return new UpdateUserResponseModel();
        }
    }

    public interface IUpdateUserRequestBoundary :
        IRequestBoundary<UpdateUserRequestModel, IResponseBoundary<UpdateUserResponseModel>> {}

    public struct UpdateUserRequestModel : IRequestModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public struct UpdateUserResponseModel : IResponseModel {}
}