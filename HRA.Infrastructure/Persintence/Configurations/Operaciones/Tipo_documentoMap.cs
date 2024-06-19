using HRA.Domain.Entities.Operaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Operaciones
{
    public class Tipo_documentoMap : IEntityTypeConfiguration<Tipo_documento>
    {
        public void Configure(EntityTypeBuilder<Tipo_documento> builder)
        {
            // table
            builder.ToTable("tipo_documento", "operaciones");

            // key
            builder.HasKey(t => t.I_ID_TIPO_DOC);

            //Properties
            builder.Property(t => t.I_ID_TIPO_DOC)
                .IsRequired()
                .HasColumnName("I_ID_TIPO_DOC")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.V_ABREVIATURA)
                .HasColumnName("V_ABREVIATURA")
                .HasColumnType("varchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.V_NOMBRE_DOC)
                .IsRequired()
                .HasColumnName("V_NOMBRE_DOC")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.I_NRO_DIGITOS)
                .HasColumnName("I_NRO_DIGITOS")
                .HasColumnType("int");

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

            builder.Property(t => t.B_ESTADO)
                .IsRequired()
                .HasColumnName("B_ESTADO")
                .HasColumnType("char(1)")
                .HasMaxLength(1);
        }

        public struct Table
        {
            public const string Schema = "operaciones";
            public const string Name = "tipo_documento";
        }

        public struct Columns
        {
            public const string I_ID_TIPO_DOC = "I_ID_TIPO_DOC";
            public const string V_ABREVIATURA = "V_ABREVIATURA";
            public const string V_NOMBRE_DOC = "V_NOMBRE_DOC";
            public const string I_NRO_DIGITOS = "I_NRO_DIGITOS";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
            public const string B_ESTADO = "B_ESTADO";
        }
    }
}
