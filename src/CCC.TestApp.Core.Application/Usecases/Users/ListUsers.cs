using System.Collections.Generic;
using System.Linq;
using CCC.TestApp.Core.Application.DALInterfaces;
using CCC.TestApp.Core.Application.Services;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class ListUsersInteractor : UserInteractor, ListUsersRequestBoundary
    {
        readonly Mapper _mapper;

        public ListUsersInteractor(UserRepository userRepository, Mapper mapper) : base(userRepository) {
            _mapper = mapper;
        }

        public void Invoke(ListUsersRequestModel inputModel, ResponseBoundary<ListUsersResponseModel> responder) {
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

    public interface ListUsersRequestBoundary :
        RequestBoundary<ListUsersRequestModel, ResponseBoundary<ListUsersResponseModel>> {}

    public struct ListUsersResponseModel : ResponseModel
    {
        public List<ShowUserResponseModel> Users { get; set; }
    }

    public struct ListUsersRequestModel : RequestModel {}
}