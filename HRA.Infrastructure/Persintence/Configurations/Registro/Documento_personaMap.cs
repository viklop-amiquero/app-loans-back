using HRA.Domain.Entities.Registro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Registro
{
    public class Documento_personaMap : IEntityTypeConfiguration<Documento_persona>
    {
        public void Configure(EntityTypeBuilder<Documento_persona> builder)
        {
            // table
            builder.ToTable("documento_persona", "registro");

            // key
            builder.HasKey(t => t.I_ID_DOCUMENTO_PERS);

            //Properties
            builder.Property(t => t.I_ID_DOCUMENTO_PERS)
                .IsRequired()
                .HasColumnName("I_ID_DOCUMENTO_PERS")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.I_ID_TIPO_DOC)
                .IsRequired()
                .HasColumnName("I_ID_TIPO_DOC")
                .HasColumnType("int");

            builder.Property(t => t.I_ID_PERSONA)
                .IsRequired()
                .HasColumnName("I_ID_PERSONA")
                .HasColumnType("int");

            builder.Property(t => t.V_NRO_DOCUMENTO)
               .IsRequired()
               .HasColumnName("V_NRO_DOCUMENTO")
               .HasColumnType("varchar(20)")
               .HasMaxLength(20);

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
            public const string Schema = "registro";
            public const string Name = "documento_persona";
        }

        public struct Columns
        {
            public const string I_ID_DOCUMENTO_PERS = "I_ID_DOCUMENTO_PERS";
            public const string I_ID_TIPO_DOC = "I_ID_TIPO_DOC";
            public const string I_ID_PERSONA = "I_ID_PERSONA";
            public const string V_NRO_DOCUMENTO = "V_NRO_DOCUMENTO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
            public const string B_ESTADO = "B_ESTADO";
        }
    }
}
