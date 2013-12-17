namespace CCC.TestApp.Core.Application.Usecases
{
    public interface IResponseBoundary {}

    public interface IResponseBoundary<in TResponseModel> : IResponseBoundary where TResponseModel : IResponseModel
    {
        void Respond(TResponseModel model);
    }

    public interface IResponseModel {}
}