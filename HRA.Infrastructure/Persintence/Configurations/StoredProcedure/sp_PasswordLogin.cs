using HRA.Domain.EntitiesStoreProcedure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA.Infrastructure.Persintence.Configurations.StoredProcedure
{
    public class sp_PasswordLogin : IEntityTypeConfiguration<entity_Password_login>
    {
        public void Configure(EntityTypeBuilder<entity_Password_login> builder)
        {
            builder.HasNoKey();
        }
    }
}
