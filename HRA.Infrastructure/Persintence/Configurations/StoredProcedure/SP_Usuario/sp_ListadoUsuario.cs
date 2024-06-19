using HRA.Domain.EntitiesStoreProcedure.SP_Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure.SP_Usuario
{
    public class sp_ListadoUsuario : IEntityTypeConfiguration<entity_Listado_usuario>
    {
        public void Configure(EntityTypeBuilder<entity_Listado_usuario> builder)
        {
            builder.HasNoKey();
        }
    }
}
