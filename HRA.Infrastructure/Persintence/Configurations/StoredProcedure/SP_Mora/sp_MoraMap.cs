using HRA.Domain.EntitiesStoreProcedure.SP_Mora;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure.SP_Mora
{
    public class sp_MoraMap : IEntityTypeConfiguration<entity_Listado_mora>
    {
        public void Configure(EntityTypeBuilder<entity_Listado_mora> builder)
        {
            builder.HasNoKey();
        }
    }
}
