using System;
using CCC.TestApp.Core.Application.DALInterfaces;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class UpdateUserInteractor : UserInteractor, UpdateUserRequestBoundary
    {
        public UpdateUserInteractor(UserRepository userRepository) : base(userRepository) {}

        public void Invoke(UpdateUserRequestModel inputModel, ResponseBoundary<UpdateUserResponseModel> responder) {
            UpdateUser(inputModel);
            responder.Respond(CreateResponseModel());
        }

        void UpdateUser(UpdateUserRequestModel inputModel) {
            var user = GetExistingUser(inputModel.Id);
            user.UserName = inputModel.UserName;
            user.Password = inputModel.Password;
            UserRepository.Update(user);
        }

        static UpdateUserResponseModel CreateResponseModel() {
            return new UpdateUserResponseModel();
        }
    }

    public interface UpdateUserRequestBoundary :
        RequestBoundary<UpdateUserRequestModel, ResponseBoundary<UpdateUserResponseModel>> {}

    public struct UpdateUserRequestModel : RequestModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public struct UpdateUserResponseModel : ResponseModel {}
}