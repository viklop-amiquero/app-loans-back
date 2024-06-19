using HRA.Domain.EntitiesStoreProcedure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure
{
    public class sp_Encrypt : IEntityTypeConfiguration<entity_Encrypt>
    {
        public void Configure(EntityTypeBuilder<entity_Encrypt> builder)
        {
            builder.HasNoKey();
        }
    }
}
