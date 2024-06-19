using HRA.Domain.EntitiesStoreProcedure.SP_Persona;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure.SP_Persona
{
    public class sp_ListadoPersonaMap : IEntityTypeConfiguration<entity_Listado_persona>
    {
        public void Configure(EntityTypeBuilder<entity_Listado_persona> builder)
        {
            builder.HasNoKey();
        }
    }
}
