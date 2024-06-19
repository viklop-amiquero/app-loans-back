using HRA.Domain.EntitiesStoreProcedure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure
{
    public class sp_Decipher : IEntityTypeConfiguration<entity_Decipher>
    {
        public void Configure(EntityTypeBuilder<entity_Decipher> builder)
        {
            builder.HasNoKey();
        }
    }
}
