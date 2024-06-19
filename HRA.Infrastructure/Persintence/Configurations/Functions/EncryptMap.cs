using HRA.Domain.Entities.Functions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Functions
{
    public class EncryptMap : IEntityTypeConfiguration<ENCRYPT>
    {
        public void Configure(EntityTypeBuilder<ENCRYPT> builder)
        {
            // Function
            builder.ToFunction("UFN_ENCRYPT");

            // key
            builder.HasNoKey();

            //Properties

            builder.Property(t => t.V_CHAIN)
                .HasColumnName("V_CHAIN")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);
        }
        public struct Function
        {
            public const string Schema = "seguridad";
            public const string Name = "UFN_ENCRYPT";
        }

        public struct Columns
        {
            public const string V_CHAIN = "V_CHAIN";
        }
    }
}