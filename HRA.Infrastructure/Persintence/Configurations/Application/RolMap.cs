using HRA.Domain.Entities.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Application
{
    public class RolMap : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            // table
            builder.ToTable("rol", "seguridad");

            // key
            builder.HasKey(t => t.I_ID_ROL);

            //Properties
            builder.Property(t => t.I_ID_ROL)
                .IsRequired()
                .HasColumnName("I_ID_ROL")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.V_ROL)
                .IsRequired()
                .HasColumnName("V_ROL")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.V_DESCRIPCION)
                .IsRequired()
                .HasColumnName("V_DESCRIPCION")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

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
            public const string Name = "Rol";
        }

        public struct Columns
        {
            public const string I_ID_ROL = "I_ID_ROL";
            public const string V_ROL = "V_ROL";
            public const string V_DESCRIPCION = "V_DESCRIPCION";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
