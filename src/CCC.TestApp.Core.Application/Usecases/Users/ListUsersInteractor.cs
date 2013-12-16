using System.Collections.Generic;
using CCC.TestApp.Core.Application.DALInterfaces;
using CCC.TestApp.Core.Domain.Entities;

namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class ListUsersInteractor : UserInteractor, IRequestBoundary<ListUsersRequestModel>
    {
        readonly IResponseBoundary<ListUsersResponseModel> _responder;

        public ListUsersInteractor(IUserRepository userRepository, IResponseBoundary<ListUsersResponseModel> responder)
            : base(userRepository) {
            _responder = responder;
        }

        public void Invoke(ListUsersRequestModel inputModel) {
            _responder.Respond(new ListUsersResponseModel {Users = UserRepository.All()});
        }
    }

    public struct ListUsersResponseModel
    {
        public List<User> Users { get; set; }
    }

    public struct ListUsersRequestModel {}
}