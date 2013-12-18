namespace CCC.TestApp.Core.Application.Services
{
    public interface IMapper
    {
        TDestination Map<TDestination>(object obj);
        TDestination Map<TSource, TDestination>(TSource obj, TDestination obj2);

        TDestination DynamicMap<TDestination>(object obj);
        void DynamicMap<TSource, TDestination>(TSource obj, TDestination obj2);
    }
}