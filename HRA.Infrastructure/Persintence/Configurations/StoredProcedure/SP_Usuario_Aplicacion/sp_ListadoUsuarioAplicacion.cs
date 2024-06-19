using HRA.Domain.EntitiesStoreProcedure.SP_Usuario_Aplicacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure.SP_Usuario_Aplicacion
{
    public class sp_ListadoUsuarioAplicacion : IEntityTypeConfiguration<entity_Listado_usuario_app>
    {
        public void Configure(EntityTypeBuilder<entity_Listado_usuario_app> builder)
        {
            builder.HasNoKey();
        }
    }
}
