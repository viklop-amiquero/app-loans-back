using HRA.Domain.EntitiesStoreProcedure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure
{
    internal class sp_Past_pass : IEntityTypeConfiguration<entity_Past_pass>
    {
        public void Configure(EntityTypeBuilder<entity_Past_pass> builder)
        {
            builder.HasNoKey();
        }
    }
}
