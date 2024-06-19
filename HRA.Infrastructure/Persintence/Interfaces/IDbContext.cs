using HRA.Domain.Common;
using LinqToDB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HRA.Infrastructure.Persistence.Interfaces
{
    public partial interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        int SaveChanges();
        void Detach<TEntity>(TEntity entity) where TEntity : BaseEntity;

        DataConnection GetConnection();

        DatabaseFacade Database { get; }
    }
}
