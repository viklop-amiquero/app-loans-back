using HRA.Domain.Entities.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Application
{
    public class Aplicacion_rol_menuMap : IEntityTypeConfiguration<Aplicacion_Rol_Menu>
    {
        public void Configure(EntityTypeBuilder<Aplicacion_Rol_Menu> builder)
        {
            // table
            builder.ToTable("Aplicacion_Rol_Menu", "seguridad");

            //key
            builder.HasKey(t => t.I_ID_APLICACION_ROL_MENU);

            //Properties
            builder.Property(t => t.I_ID_APLICACION_ROL_MENU)
                .IsRequired()
                .HasColumnName("I_ID_APLICACION_ROL_MENU")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.I_ID_MENU)
                .IsRequired()
                .HasColumnName("I_ID_MENU")
                .HasColumnType("int");

            builder.Property(t => t.I_ID_ROL)
                .IsRequired()
                .HasColumnName("I_ID_ROL")
                .HasColumnType("int");

            builder.Property(t => t.I_ID_PERMISO)
                .IsRequired()
                .HasColumnName("I_ID_PERMISO")
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
            public const string Schema = "seguridad";
            public const string Name = "Aplicacion_Rol_Menu";
        }

        public struct Columns
        {
            public const string I_ID_APLICACION_ROL_MENU = "I_ID_APLICACION_ROL_MENU";
            public const string I_ID_MENU = "I_ID_MENU";
            public const string I_ID_ROL = "I_ID_ROL";
            public const string I_ID_PERMISO = "I_ID_PERMISO";
            public const string V_DESCRIPCION = "V_DESCRIPCION";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
