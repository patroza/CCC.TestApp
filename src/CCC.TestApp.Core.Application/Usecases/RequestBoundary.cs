namespace CCC.TestApp.Core.Application.Usecases
{
    public interface RequestBoundary {}

    public interface RequestBoundary<in TInputModel, in TRespondModel> : RequestBoundary
        where TRespondModel : ResponseBoundary where TInputModel : RequestModel
    {
        void Invoke(TInputModel inputModel, TRespondModel responseModel);
    }

    public interface RequestModel {}
}