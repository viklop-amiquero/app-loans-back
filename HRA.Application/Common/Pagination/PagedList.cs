using HRA.Application.Common.Interfaces;
using HRA.Transversal.Extensions;

namespace HRA.Application.Common.Pagination
{
    //Esta clase implementa al interface IPagedList
    public class PagedList : IPagedList
    {
        public PagedResults<T> CreatePagedGenericResults<T>(
        IQueryable<T> queryable,
        int page,
        int pageSize,
        string orderBy,
        bool ascending)
        {
            if (page <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("La página y el tamaño de página deben ser mayores que cero..");
            }

            var skipAmount = pageSize * (page - 1);
            var totalNumberOfRecords = queryable.Count();

            IQueryable<T> resultsQuery;

            if (!string.IsNullOrEmpty(orderBy))
            {
                resultsQuery = queryable.OrderByPropertyOrField(orderBy, ascending);
            }
            else
            {
                // Default ordering if orderBy is not provided
                resultsQuery = queryable;
            }

            var results = resultsQuery.Skip(skipAmount).Take(pageSize).ToList();

            var totalPageCount = (totalNumberOfRecords + pageSize - 1) / pageSize;

            return new PagedResults<T>
            {
                Data = results,
                PageNumber = page,
                PageSize = pageSize,
                TotalNumberOfPages = totalPageCount,
                TotalNumberOfRecords = totalNumberOfRecords
            };
        }
    }
}
