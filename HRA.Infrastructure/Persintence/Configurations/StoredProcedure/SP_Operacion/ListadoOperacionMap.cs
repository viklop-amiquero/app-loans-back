using HRA.Domain.EntitiesStoreProcedure.SP_Operacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure.SP_Operacion
{
    public class sp_ListadoOperacionMap : IEntityTypeConfiguration<entity_listado_operacion>
    {
        public void Configure(EntityTypeBuilder<entity_listado_operacion> builder)
        {
            builder.HasNoKey();
        }
    }
}
