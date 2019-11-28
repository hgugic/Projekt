using AutoMapper;
using System.Collections.Generic;
using X.PagedList;

namespace Projekt.MVC.Mappings
{
    public static class PagedListMappingExtension
    {
        //Ekstenzija je napravljena jer nije moguće direktno mapiranje X.PagedList (razlog: konstruktor) 
        public static IPagedList<TDestination> ToMappedPagedList<TSource, TDestination>(this IPagedList<TSource> list, IMapper mapper)
        {
            IEnumerable<TDestination> sourceList = mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(list);
            IPagedList<TDestination> pagedResult = new StaticPagedList<TDestination>(sourceList, list.GetMetaData());
            return pagedResult;

        }
    }
}
