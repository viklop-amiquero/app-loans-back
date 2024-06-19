using HRA.Domain.EntitiesStoreProcedure.SP_Cuenta;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure.SP_Cuenta
{
    public class sp_ListadoCuentaMap : IEntityTypeConfiguration<entity_listado_cuenta>
    {
        public void Configure(EntityTypeBuilder<entity_listado_cuenta> builder)
        {
            builder.HasNoKey();
        }
    }
}
