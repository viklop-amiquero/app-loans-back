using HRA.Domain.EntitiesStoreProcedure.SP_Rol;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure.SP_Rol
{
    public class sp_RolMap : IEntityTypeConfiguration<entity_Listado_rol>
    {
        public void Configure(EntityTypeBuilder<entity_Listado_rol> builder)
        {
            builder.HasNoKey();
        }
    }
}
