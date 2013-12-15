namespace CCC.TestApp.Core.Application.Usecases
{
    public interface IResponseBoundary<in TResponseModel>
    {
        void Respond(TResponseModel model);
    }
}