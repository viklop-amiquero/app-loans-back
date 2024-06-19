using HRA.Domain.EntitiesStoreProcedure.SP_Aplicacion;
using HRA.Domain.EntitiesStoreProcedure.SP_SubCuota;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure.SP_SubCuota
{
    public class sp_subCuotaMap : IEntityTypeConfiguration<entity_listado_sub_cuota>
    {
        public void Configure(EntityTypeBuilder<entity_listado_sub_cuota> builder)
        {
            builder.HasNoKey();
        }
    }
}
