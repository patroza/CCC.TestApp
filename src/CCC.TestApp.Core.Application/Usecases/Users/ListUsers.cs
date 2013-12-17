using System.Collections.Generic;
using System.Linq;
using CCC.TestApp.Core.Application.DALInterfaces;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class ListUsersInteractor : UserInteractor, IListUsersRequestBoundary
    {
        public ListUsersInteractor(IUserRepository userRepository) : base(userRepository) {}

        public void Invoke(ListUsersRequestModel inputModel, IResponseBoundary<ListUsersResponseModel> responder) {
            responder.Respond(CreateResponseModel());
        }

        ListUsersResponseModel CreateResponseModel() {
            return new ListUsersResponseModel {
                Users = GetUsers()
            };
        }

        List<object> GetUsers() {
            return UserRepository.All().Select(x => new {x.Id, x.UserName}).ToList<object>();
        }
    }

    public interface IListUsersRequestBoundary :
        IRequestBoundary<ListUsersRequestModel, IResponseBoundary<ListUsersResponseModel>> {}

    public struct ListUsersResponseModel : IResponseModel
    {
        public List<object> Users { get; set; }
    }

    public struct ListUsersRequestModel : IRequestModel {}
}