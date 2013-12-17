namespace CCC.TestApp.Core.Application.Usecases
{
    public interface IRequestBoundary {}

    public interface IRequestBoundary<in TInputModel, in TRespondModel> : IRequestBoundary
        where TRespondModel : IResponseBoundary where TInputModel : IRequestModel
    {
        void Invoke(TInputModel inputModel, TRespondModel responseModel);
    }

    public interface IRequestModel {}
}