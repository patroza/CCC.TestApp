using System;
using CCC.TestApp.Core.Application.DALInterfaces;
using CCC.TestApp.Core.Application.Services;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class ShowUserInteractor : UserInteractor, ShowUserRequestBoundary
    {
        readonly Mapper _mapper;

        public ShowUserInteractor(UserRepository userRepository, Mapper mapper) : base(userRepository) {
            _mapper = mapper;
        }

        public void Invoke(ShowUserRequestModel inputModel, ResponseBoundary<ShowUserResponseModel> responder) {
            responder.Respond(CreateResponseModel(inputModel));
        }

        ShowUserResponseModel CreateResponseModel(ShowUserRequestModel inputModel) {
            var user = GetExistingUser(inputModel.UserId);
            return _mapper.DynamicMap<ShowUserResponseModel>(user);
        }
    }

    public interface ShowUserRequestBoundary :
        RequestBoundary<ShowUserRequestModel, ResponseBoundary<ShowUserResponseModel>> {}

    public struct ShowUserResponseModel : ResponseModel
    {
        public string UserName { get; set; }
        public Guid Id { get; set; }
        public string Password { get; set; }
    }

    public struct ShowUserRequestModel : RequestModel
    {
        public Guid UserId;

        public ShowUserRequestModel(Guid userId) {
            UserId = userId;
        }
    }
}