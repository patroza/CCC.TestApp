namespace CCC.TestApp.Core.Application.Usecases
{
    public interface IRequestBoundary<in TInputModel>
    {
        void Invoke(TInputModel inputModel);
    }
}