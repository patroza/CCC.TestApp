namespace CCC.TestApp.Core.Application.Usecases.Users
{
    public class CreateUserInteractor : ICreateUserRequestBoundary
    {
        public void Invoke(CreateUserRequestModel inputModel, ICreateUserResponseBoundary responder) {
            responder.Respond(new CreateUserResponseModel());
        }
    }

    public struct CreateUserRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public struct CreateUserResponseModel {}

    public interface ICreateUserRequestBoundary : IRequestBoundary<CreateUserRequestModel, ICreateUserResponseBoundary> {}

    public interface ICreateUserResponseBoundary : IResponseBoundary<CreateUserResponseModel> {}
}