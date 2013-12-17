using System;
using CCC.TestApp.Core.Application.DALInterfaces;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class ShowUserInteractor : UserInteractor, IShowUserRequestBoundary
    {
        public ShowUserInteractor(IUserRepository userRepository) : base(userRepository) {}

        public void Invoke(ShowUserRequestModel inputModel, IResponseBoundary<ShowUserResponseModel> responder) {
            responder.Respond(CreateResponseModel(inputModel));
        }

        ShowUserResponseModel CreateResponseModel(ShowUserRequestModel inputModel) {
            var user = GetExistingUser(inputModel.UserId);
            return new ShowUserResponseModel {UserName = user.UserName, Id = user.Id};
        }
    }

    public interface IShowUserRequestBoundary :
        IRequestBoundary<ShowUserRequestModel, IResponseBoundary<ShowUserResponseModel>> {}

    public struct ShowUserResponseModel : IResponseModel
    {
        public string UserName { get; set; }
        public Guid Id { get; set; }
    }

    public struct ShowUserRequestModel : IRequestModel
    {
        public Guid UserId;

        public ShowUserRequestModel(Guid userId) {
            UserId = userId;
        }
    }
}