using HRA.Domain.Entities.Operaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Operaciones
{
    public class PuestoMap : IEntityTypeConfiguration<Puesto>
    {
        public void Configure(EntityTypeBuilder<Puesto> builder)
        {
            // Table
            builder.ToTable("puesto", "operaciones");

            // Key
            builder.HasKey(e => e.I_ID_PUESTO);

            // Properties
            builder.Property(e => e.I_ID_PUESTO)
                .HasColumnName("I_ID_PUESTO");

            builder.Property(e => e.V_NOMBRE)
                .IsRequired()
                .HasColumnName("V_NOMBRE")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(e => e.V_DESCRIPCION)
                .HasColumnName("V_DESCRIPCION")
                .HasColumnType("varchar(200)")
                .HasMaxLength(200);

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
            public const string Schema = "operaciones";
            public const string Name = "Puesto";
        }

        public struct Columns
        {
            public const string I_ID_PUESTO = "I_ID_PUESTO";
            public const string V_NOMBRE = "V_NOMBRE";
            public const string V_DESCRIPCION = "V_DESCRIPCION";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
