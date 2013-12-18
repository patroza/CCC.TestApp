using System.Collections.Generic;
using System.Linq;
using CCC.TestApp.Core.Application.DALInterfaces;
using CCC.TestApp.Core.Application.Services;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class ListUsersInteractor : UserInteractor, IListUsersRequestBoundary
    {
        readonly IMapper _mapper;

        public ListUsersInteractor(IUserRepository userRepository, IMapper mapper) : base(userRepository) {
            _mapper = mapper;
        }

        public void Invoke(ListUsersRequestModel inputModel, IResponseBoundary<ListUsersResponseModel> responder) {
            responder.Respond(CreateResponseModel());
        }

        ListUsersResponseModel CreateResponseModel() {
            return new ListUsersResponseModel {
                Users = GetUsers()
            };
        }

        List<ShowUserResponseModel> GetUsers() {
            return UserRepository.All().Select(_mapper.DynamicMap<ShowUserResponseModel>).ToList();
        }
    }

    public interface IListUsersRequestBoundary :
        IRequestBoundary<ListUsersRequestModel, IResponseBoundary<ListUsersResponseModel>> {}

    public struct ListUsersResponseModel : IResponseModel
    {
        public List<ShowUserResponseModel> Users { get; set; }
    }

    public struct ListUsersRequestModel : IRequestModel {}
}