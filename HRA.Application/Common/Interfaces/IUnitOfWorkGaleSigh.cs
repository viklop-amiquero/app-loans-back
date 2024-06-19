using HRA.Domain.Common;

namespace HRA.Application.Common.Interfaces
{
    public interface IUnitOfWorkGaleSigh
    {
        Task CommitChanges();
        Task<IList<T>> ExcuteStoreQuery<T>(string storeProcedureName, params object[] parameters) where T : BaseEntity;
        Task<IList<T>> StoreQuery<T>(string storeProcedureName, params object[] parameters) where T : BaseEntity;
        Task<(IList<T>, int)> ExcuteStoreQueryList<T>(string storeProcedureName, params object[] parameters) where T : BaseEntity;
    }
}
