using HRA.Domain.Entities.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Application
{
    public class PermisoMap : IEntityTypeConfiguration<Permiso>
    {
        public void Configure(EntityTypeBuilder<Permiso> builder)
        {
            // table
            builder.ToTable("Permiso", "seguridad");

            //key
            builder.HasKey(t => t.I_ID_PERMISO);

            //Properties
            builder.Property(t => t.I_ID_PERMISO)
                .IsRequired()
                .HasColumnName("I_ID_PERMISO")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.I_C)
                .IsRequired()
                .HasColumnName("I_C")
                .HasColumnType("int");

            builder.Property(t => t.I_R)
                .IsRequired()
                .HasColumnName("I_R")
                .HasColumnType("int");

            builder.Property(t => t.I_U)
                .IsRequired()
                .HasColumnName("I_U")
                .HasColumnType("int");

            builder.Property(t => t.I_D)
                .IsRequired()
                .HasColumnName("I_D")
                .HasColumnType("int");

            builder.Property(t => t.V_DESCRIPCION)
             .HasColumnName("V_DESCRIPCION")
             .HasColumnType("varchar(80)")
             .HasMaxLength(80);

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
            public const string Name = "Permiso";
        }

        public struct Columns
        {
            public const string I_ID_PERMISO = "I_ID_PERMISO";
            public const string I_C = "I_C";
            public const string I_R = "I_R";
            public const string I_U = "I_U";
            public const string I_D = "I_D";
            public const string V_DESCRIPCION = "V_DESCRIPCION";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
