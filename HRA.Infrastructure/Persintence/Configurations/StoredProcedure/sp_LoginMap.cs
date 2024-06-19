using HRA.Domain.EntitiesStoreProcedure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure
{
    public class sp_LoginMap : IEntityTypeConfiguration<entity_Login>
    {
        public void Configure(EntityTypeBuilder<entity_Login> builder)
        {
            builder.HasNoKey();
        }
    }
}
