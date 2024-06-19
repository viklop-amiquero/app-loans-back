using HRA.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Security
{
    public class ClaveMap : IEntityTypeConfiguration<Clave>
    {
        public void Configure(EntityTypeBuilder<Clave> builder)
        {
            // table
            builder.ToTable("Clave", "seguridad");

            // key
            builder.HasKey(t => t.I_ID_CLAVE);

            //Properties
            builder.Property(t => t.I_ID_CLAVE)
                .IsRequired()
                .HasColumnName("I_ID_CLAVE")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.I_ID_USUARIO)
                .IsRequired()
                .HasColumnName("I_ID_USUARIO")
                .HasColumnType("int");

            builder.Property(t => t.V_CLAVE_HASH)
                .IsRequired()
                .HasColumnName("V_CLAVE_HASH")
                .HasColumnType("varbinary(max)")
                .HasMaxLength(500);

            builder.Property(t => t.B_RESTABLECER)
                .HasColumnName("B_RESTABLECER")
                .HasColumnType("char(1)")
                .HasMaxLength(1);

            builder.Property(t => t.B_ESTADO)
                .IsRequired()
                .HasColumnName("B_ESTADO")
                .HasColumnType("char(1)")
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
            public const string Name = "Clave";
        }

        public struct Columns
        {
            public const string I_ID_CLAVE = "I_ID_CLAVE";
            public const string I_ID_USUARIO = "I_ID_USUARIO";
            public const string V_CLAVE_HASH = "V_CLAVE_HASH";
            public const string B_RESTABLECER = "B_RESTABLECER";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
