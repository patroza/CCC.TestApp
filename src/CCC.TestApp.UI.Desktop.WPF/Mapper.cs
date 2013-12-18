using AutoMapper;
using CCC.TestApp.Core.Application;

namespace CCC.TestApp.UI.Desktop.WPF
{
    public class SixMapper : IMapper
    {
        public TDestination Map<TDestination>(object obj) {
            return Mapper.Map<TDestination>(obj);
        }

        public TDestination Map<TSource, TDestination>(TSource obj, TDestination obj2) {
            return Mapper.Map(obj, obj2);
        }

        public TDestination DynamicMap<TDestination>(object obj) {
            return Mapper.DynamicMap<TDestination>(obj);
        }

        public void DynamicMap<TSource, TDestination>(TSource obj, TDestination obj2) {
            Mapper.DynamicMap(obj, obj2);
        }
    }
}