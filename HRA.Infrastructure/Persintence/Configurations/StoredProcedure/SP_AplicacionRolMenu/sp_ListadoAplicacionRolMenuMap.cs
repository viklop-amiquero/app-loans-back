using HRA.Domain.EntitiesStoreProcedure.SP_AplicacionRolMenu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure.SP_AplicacionRolMenu
{
    public class sp_ListadoAplicacionRolMenuMap : IEntityTypeConfiguration<entity_Listado_aplicacion_rol_menu>
    {
        public void Configure(EntityTypeBuilder<entity_Listado_aplicacion_rol_menu> builder)
        {
            builder.HasNoKey();
        }
    }
}
