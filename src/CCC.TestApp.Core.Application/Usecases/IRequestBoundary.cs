namespace CCC.TestApp.Core.Application.Usecases
{
    public interface IRequestBoundary<in TInputModel, in TResponseBoundary>
    {
        void Invoke(TInputModel inputModel, TResponseBoundary responder);
    }
}