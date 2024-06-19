using HRA.Domain.Entities.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Application
{
    public class AplicacionMap : IEntityTypeConfiguration<Aplicacion>
    {
        public void Configure(EntityTypeBuilder<Aplicacion> builder)
        {
            // table
            builder.ToTable("Aplicacion", "seguridad");

            // key
            builder.HasKey(t => t.I_ID_APLICACION);

            //Properties
            builder.Property(t => t.I_ID_APLICACION)
                .IsRequired()
                .HasColumnName("I_ID_APLICACION")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.V_APLICACION)
                .IsRequired()
                .HasColumnName("V_APLICACION")
                .HasColumnType("varchar(200)")
                .HasMaxLength(200);

            builder.Property(t => t.V_ACRONIMO)
                .HasColumnName("V_ACRONIMO")
                .HasColumnType("varchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.V_DESCRIPCION)
                .HasColumnName("V_DESCRIPCION")
                .HasColumnType("varchar(200)")
                .HasMaxLength(200);

            builder.Property(t => t.V_URL)
               .HasColumnName("V_URL")
               .HasColumnType("varchar(250)")
               .HasMaxLength(250);

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
            public const string Name = "Aplicacion";
        }

        public struct Columns
        {
            public const string I_ID_APLICACION = "I_ID_APLICACION";
            public const string V_APLICACION = "V_APLICACION";
            public const string V_ACRONIMO = "V_ACRONIMO";
            public const string V_DESCRIPCION = "V_DESCRIPCION";
            public const string V_URL = "V_URL";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
