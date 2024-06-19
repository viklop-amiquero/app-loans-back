using HRA.Domain.Entities.Registro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Registro
{
    public class Puesto_personaMap : IEntityTypeConfiguration<Puesto_persona>
    {
        public void Configure(EntityTypeBuilder<Puesto_persona> builder)
        {
            // Table
            builder.ToTable("puesto_persona", "registro");

            // Key
            builder.HasKey(e => e.I_ID_PUESTO_PERS);

            // Properties
            builder.Property(e => e.I_ID_PUESTO_PERS)
                .IsRequired()
                .HasColumnName("I_ID_PUESTO_PERS")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.I_ID_PERSONA)
                .IsRequired()
                .HasColumnName("I_ID_PERSONA")
                .HasColumnType("int");

            builder.Property(e => e.I_ID_PUESTO)
                .IsRequired()
                .HasColumnName("I_ID_PUESTO")
                .HasColumnType("int");

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
            public const string Schema = "registro";
            public const string Name = "puesto_persona";
        }

        public struct Columns
        {
            public const string I_ID_PUESTO_PERS = "I_ID_PUESTO_PERS";
            public const string I_ID_PERSONA = "I_ID_PERSONA";
            public const string I_ID_PUESTO = "I_ID_PUESTO";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
