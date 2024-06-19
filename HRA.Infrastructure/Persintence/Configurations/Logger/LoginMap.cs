using HRA.Domain.Entities.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Logger
{
    public class LoginMap : IEntityTypeConfiguration<Login>
    {
        public void Configure(EntityTypeBuilder<Login> builder)
        {
            // table
            builder.ToTable("login", "logger");

            // key
            builder.HasKey(t => t.I_ID_LOGIN);

            //Properties
            builder.Property(t => t.I_ID_LOGIN)
                .IsRequired()
                .HasColumnName("I_ID_LOGIN")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.I_ID_USER)
                .HasColumnName("I_ID_USER")
                .HasColumnType("int");

            builder.Property(t => t.V_LOGIN)
                .HasColumnName("V_LOGIN")
                .HasColumnType("varchar(13)")
                .HasMaxLength(13);

            builder.Property(t => t.V_ID_ROL)
                .HasColumnName("V_ID_ROL")
                .HasColumnType("varchar(150)")
                .HasMaxLength(150);

            builder.Property(t => t.V_IP)
                .HasColumnName("V_IP")
                .HasColumnType("varchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.I_INTENTO)
                .IsRequired()
                .HasColumnName("I_INTENTO")
                .HasColumnType("int");

            builder.Property(t => t.D_FECHA_REGISTRO)
                .HasColumnName("D_FECHA_REGISTRO")
                .HasColumnType("datetime");
        }

        public struct Table
        {
            public const string Schema = "logger";
            public const string Name = "login";
        }

        public struct Columns
        {
            public const string I_ID_LOGIN = "I_ID_LOGIN";
            public const string I_ID_USER = "I_ID_USER";
            public const string V_LOGIN = "V_LOGIN";
            public const string V_ID_ROL = "V_ID_ROL";
            public const string V_IP = "V_IP";
            public const string I_INTENTO = "I_INTENTO";
            public const string D_FECHA_REGISTRO = "D_FECHA_REGISTRO";
        }
    }
}
