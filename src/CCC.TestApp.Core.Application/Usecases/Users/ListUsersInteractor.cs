using System.Collections.Generic;
using System.Linq;
using CCC.TestApp.Core.Application.DALInterfaces;

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
            _responder.Respond(new ListUsersResponseModel {
                Users = UserRepository.All().Select(x => new {x.Id, x.UserName}).ToList<object>()
            });
        }
    }

    public struct ListUsersResponseModel
    {
        public List<object> Users { get; set; }
    }

    public struct ListUsersRequestModel {}
}