using HRA.Domain.EntitiesStoreProcedure.SP_Aplicacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure.SP_Aplicacion
{
    public class sp_ListadoAplicacionMap : IEntityTypeConfiguration<entity_Listado_aplicacion>
    {
        public void Configure(EntityTypeBuilder<entity_Listado_aplicacion> builder)
        {
            builder.HasNoKey();
        }
    }
}
