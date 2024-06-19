using HRA.Domain.Entities.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Application
{
    public class User_aplicacionMap : IEntityTypeConfiguration<Usuario_Aplicacion>
    {
        public void Configure(EntityTypeBuilder<Usuario_Aplicacion> builder)
        {
            // table
            builder.ToTable("Usuario_Aplicacion", "seguridad");

            // key
            builder.HasKey(t => t.I_ID_APLICACION_USUARIO);

            //Properties
            builder.Property(t => t.I_ID_APLICACION_USUARIO)
                .IsRequired()
                .HasColumnName("I_ID_APLICACION_USUARIO")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.I_ID_USUARIO)
                .IsRequired()
                .HasColumnName("I_ID_USUARIO")
                .HasColumnType("int");

            builder.Property(t => t.I_ID_APLICACION_ROL_MENU)
                .IsRequired()
                .HasColumnName("I_ID_APLICACION_ROL_MENU")
                .HasColumnType("int");

            builder.Property(t => t.D_FECHA_INICIO)
                .IsRequired()
                .HasColumnName("D_FECHA_INICIO")
                .HasColumnType("date");

            builder.Property(t => t.D_FECHA_FIN)
               .HasColumnName("D_FECHA_FIN")
               .HasColumnType("date");

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
            public const string Name = "Usuario_Aplicacion";
        }

        public struct Columns
        {
            public const string I_ID_APLICACION_USUARIO = "I_ID_APLICACION_USUARIO";
            public const string I_ID_USUARIO = "I_ID_USUARIO";
            public const string I_ID_APLICACION_ROL_MENU = "I_ID_APLICACION_ROL_MENU";
            public const string D_FECHA_INICIO = "D_FECHA_INICIO";
            public const string D_FECHA_FIN = "D_FECHA_FIN";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
