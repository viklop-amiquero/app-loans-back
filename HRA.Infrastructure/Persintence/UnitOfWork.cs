using HRA.Application.Common.Interfaces;
using HRA.Domain.Common;
using HRA.Infrastructure.Persistence.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace HRA.Infrastructure.Persintence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDbContextTransaction _tx;
        private IDbContext _ctx;

        public UnitOfWork(IDbContext ctx)
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
            var parameterReturn = new SqlParameter
            {
                ParameterName = "@returnValue",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
                Value = 0
            };
            parameters = parameters.Append(parameterReturn).ToArray();
            return (await _ctx.Set<T>().FromSqlRaw($"EXEC {storeProcedureName}", parameters).ToListAsync(), Convert.ToInt32(parameterReturn.Value));
        }

        public async Task<IList<T>> StoreQuery<T>(string storeProcedureName, params object[] parameters) where T : BaseEntity
        {
            var sqlParameters = parameters.Select((param, index) => new SqlParameter($"@p{index}", param ?? DBNull.Value)).ToArray();
            return await _ctx.Set<T>().FromSqlRaw(storeProcedureName, sqlParameters).ToListAsync();
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
