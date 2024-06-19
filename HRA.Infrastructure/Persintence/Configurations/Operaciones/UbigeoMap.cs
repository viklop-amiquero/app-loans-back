using HRA.Domain.Entities.Operaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Operaciones
{
    public class UbigeoMap : IEntityTypeConfiguration<Ubigeo>
    {
        public void Configure(EntityTypeBuilder<Ubigeo> builder)
        {
            // Table
            builder.ToTable("ubigeo", "operaciones");

            // Key
            builder.HasKey(e => e.I_ID_UBIGEO);

            // Properties
            builder.Property(e => e.I_ID_UBIGEO)
                .IsRequired()
                .HasColumnName("I_ID_UBIGEO")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.V_CODIGO_DEPARTAMENTO)
                .IsRequired()
                .HasColumnName("V_CODIGO_DEPARTAMENTO")
                .HasColumnType("varchar(8)")
                .HasMaxLength(50);

            builder.Property(e => e.V_DEPARTAMENTO)
                .HasColumnName("V_DEPARTAMENTO")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(e => e.V_CODIGO_PROVINCIA)
                .HasColumnName("V_CODIGO_PROVINCIA")
                .HasColumnType("varchar(10)")
                .HasMaxLength(10);

            builder.Property(e => e.V_PROVINCIA)
                .HasColumnName("V_PROVINCIA")
                .HasColumnType ("varchar(50)")
                .HasMaxLength(50);

            builder.Property(e => e.V_CODIGO_DISTRITO)
                .HasColumnName("V_CODIGO_DISTRITO")
                .HasColumnType("varchar(10)")
                .HasMaxLength(10);

            builder.Property(e => e.V_DISTRITO)
                .HasColumnName("V_DISTRITO")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(e => e.V_CAPITAL)
                .HasColumnName("V_CAPITAL")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(e => e.V_ALTITUDE)
                .HasColumnName("V_ALTITUDE")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(e => e.V_LATITUDE)
                .HasColumnName("V_LATITUDE")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(e => e.V_LONGITUDE)
                .HasColumnName("V_LONGITUDE")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

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
            public const string Name = "ubigeo";
        }

        public struct Columns
        {
            public const string I_ID_UBIGEO = "I_ID_UBIGEO";
            public const string V_CODIGO_DEPARTAMENTO = "V_CODIGO_DEPARTAMENTO";
            public const string V_DEPARTAMENTO = "V_DEPARTAMENTO";
            public const string V_CODIGO_PROVINCIA = "V_CODIGO_PROVINCIA";
            public const string V_PROVINCIA = "V_PROVINCIA";
            public const string V_CODIGO_DISTRITO = "V_CODIGO_DISTRITO";
            public const string V_DISTRITO = "V_DISTRITO";
            public const string V_CAPITAL = "V_CAPITAL";
            public const string V_ALTITUDE = "V_ALTITUDE";
            public const string V_LATITUDE = "V_LATITUDE";
            public const string V_LONGITUDE = "V_LONGITUDE";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
