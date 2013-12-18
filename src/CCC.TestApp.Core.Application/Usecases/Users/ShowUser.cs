using System;
using CCC.TestApp.Core.Application.DALInterfaces;
using CCC.TestApp.Core.Application.Services;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class ShowUserInteractor : UserInteractor, IShowUserRequestBoundary
    {
        readonly IMapper _mapper;

        public ShowUserInteractor(IUserRepository userRepository, IMapper mapper) : base(userRepository) {
            _mapper = mapper;
        }

        public void Invoke(ShowUserRequestModel inputModel, IResponseBoundary<ShowUserResponseModel> responder) {
            responder.Respond(CreateResponseModel(inputModel));
        }

        ShowUserResponseModel CreateResponseModel(ShowUserRequestModel inputModel) {
            var user = GetExistingUser(inputModel.UserId);
            return _mapper.DynamicMap<ShowUserResponseModel>(user);
        }
    }

    public interface IShowUserRequestBoundary :
        IRequestBoundary<ShowUserRequestModel, IResponseBoundary<ShowUserResponseModel>> {}

    public struct ShowUserResponseModel : IResponseModel
    {
        public string UserName { get; set; }
        public Guid Id { get; set; }
        public string Password { get; set; }
    }

    public struct ShowUserRequestModel : IRequestModel
    {
        public Guid UserId;

        public ShowUserRequestModel(Guid userId) {
            UserId = userId;
        }
    }
}