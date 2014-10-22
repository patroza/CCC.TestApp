namespace CCC.TestApp.Core.Application.Usecases
{
    public interface ResponseBoundary {}

    public interface ResponseBoundary<in TResponseModel> : ResponseBoundary where TResponseModel : ResponseModel
    {
        void Respond(TResponseModel model);
    }

    public interface ResponseModel {}
}