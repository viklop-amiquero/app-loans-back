using HRA.Domain.EntitiesStoreProcedure.SP_Cuota;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure.SP_Cuota
{
    public class sp_ListadoCuotaMap : IEntityTypeConfiguration<entity_listado_cuota>
    {
        public void Configure(EntityTypeBuilder<entity_listado_cuota> builder)
        {
            builder.HasNoKey(); 
        }
    }
}
