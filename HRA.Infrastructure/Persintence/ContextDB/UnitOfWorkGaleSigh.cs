using HRA.Application.Common.Interfaces;
using HRA.Domain.Common;
using HRA.Infrastructure.Persintence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HRA.Infrastructure.Persintence
{
    public class UnitOfWorkGaleSigh : IUnitOfWorkGaleSigh, IDisposable
    {
        private readonly IDbContextTransaction _tx;
        private IDbContextGaleSigh _ctx;

        public UnitOfWorkGaleSigh(IDbContextGaleSigh ctx)
        {
            _ctx = ctx;
            _tx = ctx.Database.BeginTransaction();

        }


        public async Task CommitChanges()
        {
            try
            {
                _ctx.SaveChanges();
                await _tx.CommitAsync();
            }
            catch (Exception ex)
            {
                await _tx.RollbackAsync();
                throw ex;
            }

        }

        public async Task<IList<T>> ExcuteStoreQuery<T>(string storeProcedureName, params object[] parameters) where T : BaseEntity
        {
            return await _ctx.Set<T>().FromSqlRaw($"EXEC {storeProcedureName}", parameters).ToListAsync();
        }

        public async Task<(IList<T>, int)> ExcuteStoreQueryList<T>(string storeProcedureName, params object[] parameters) where T : BaseEntity
        {
            var parameterReturn = new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = "I_TOTAL_REG",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };
            parameters = parameters.Append(parameterReturn).ToArray();
            var resultQuery = await _ctx.Set<T>().FromSqlRaw($"EXEC {storeProcedureName}", parameters).ToListAsync();
            return (resultQuery, Convert.ToInt32(parameterReturn.Value));
        }

        public async Task<IList<T>> StoreQuery<T>(string storeProcedureName, params object[] parameters) where T : BaseEntity
        {

            var resultQuery = await _ctx.Set<T>().FromSqlRaw($"{storeProcedureName}", parameters).ToListAsync();

            return (resultQuery);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tx?.Dispose();
            }

        }
    }
}
