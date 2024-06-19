using HRA.Domain.EntitiesStoreProcedure.SP_Cuenta;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure.SP_Cuenta
{
    public class sp_PaginadoCuentaMap : IEntityTypeConfiguration<entity_paginado_cuenta>
    {
        public void Configure(EntityTypeBuilder<entity_paginado_cuenta> builder)
        {
            builder.HasNoKey();
        }
    }
}
