using HRA.Domain.EntitiesStoreProcedure.SP_Credito;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure.SP_Credito
{
    public class sp_ListadoCreditoMap : IEntityTypeConfiguration<entity_listado_credito>
    {
        public void Configure(EntityTypeBuilder<entity_listado_credito> builder)
        {
            builder.HasNoKey();
        }
    }
}
