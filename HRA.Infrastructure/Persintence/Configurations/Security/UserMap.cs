using HRA.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Security
{
    public partial class UserMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            // table
            builder.ToTable("Usuario", "seguridad");

            // key
            builder.HasKey(t => t.I_ID_USUARIO);

            //Properties
            builder.Property(t => t.I_ID_USUARIO)
                .IsRequired()
                .HasColumnName("I_ID_USUARIO")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.I_ID_PERSONA)
                .IsRequired()
                .HasColumnName("I_ID_PERSONA")
                .HasColumnType("int");

            builder.Property(t => t.V_USUARIO)
                .IsRequired()
                .HasColumnName("V_USUARIO")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.B_ESTADO)
                .HasColumnName("B_ESTADO")
                .HasColumnType("varchar(1)")
                .HasMaxLength(1);

            builder.Property(t => t.I_USUARIO_CREACION)
                .HasColumnName("I_USUARIO_CREACION")
                .HasColumnType("int");

            builder.Property(t => t.D_FECHA_CREACION)
                .HasColumnName("D_FECHA_CREACION")
                .HasColumnType("datetime");

            builder.Property(t => t.I_USUARIO_MODIFICA)
                .HasColumnName("I_USUARIO_MODIFICA")
                .HasColumnType("int");

            builder.Property(t => t.D_FECHA_MODIFICA)
                .HasColumnName("D_FECHA_MODIFICA")
                .HasColumnType("datetime");
        }

        public struct Table
        {
            public const string Schema = "seguridad";
            public const string Name = "Usuario";
        }

        public struct Columns
        {
            public const string I_ID_USUARIO = "I_ID_USUARIO";
            public const string I_ID_PERSONA = "I_ID_PERSONA";
            public const string V_USUARIO = "V_USUARIO";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
