using HRA.Application.Common.Pagination;

namespace HRA.Application.Common.Interfaces
{
    public interface IPagedList
    {
        public PagedResults<T> CreatePagedGenericResults<T>(
            IQueryable<T> queryable,
            int page,
            int pageSize,
            string orderBy,
            bool ascending
        );
    }
}
