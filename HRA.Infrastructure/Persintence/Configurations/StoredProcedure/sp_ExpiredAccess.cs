using HRA.Domain.EntitiesStoreProcedure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure
{
    public class sp_ExpiredAccess : IEntityTypeConfiguration<entity_Expired_access>
    {
        public void Configure(EntityTypeBuilder<entity_Expired_access> builder)
        {
            builder.HasNoKey();
        }
    }
}
