using HRA.Domain.Entities.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Application
{
    public class MenuMap : IEntityTypeConfiguration<Menú>
    {
        public void Configure(EntityTypeBuilder<Menú> builder)
        {
            // table
            builder.ToTable("Menu", "seguridad");

            //key
            builder.HasKey(t => t.I_ID_MENU);

            //Properties
            builder.Property(t => t.I_ID_MENU)
                .IsRequired()
                .HasColumnName("I_ID_MENU")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.I_ID_APLICACION)
                .IsRequired()
                .HasColumnName("I_ID_APLICACION")
                .HasColumnType("int");

            builder.Property(t => t.V_MENU)
                .IsRequired()
                .HasColumnName("V_MENU")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.V_DESCRIPCION)
                .HasColumnName("V_DESCRIPCION")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.V_ICONO)
                .HasColumnName("V_ICONO")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.V_RUTA)
                .HasColumnName("V_RUTA")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.V_URL)
                .HasColumnName("V_URL")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.V_NIVEL_PARENTESCO)
                .IsRequired()
                .HasColumnName("V_NIVEL_PARENTESCO")
                .HasColumnType("uniqueidentifier");

            builder.Property(t => t.V_PARENTESCO)
                .HasColumnName("V_PARENTESCO")
                .HasColumnType("varchar(200)")
                .HasMaxLength(200);

            builder.Property(t => t.I_NIVEL)
                .HasColumnName("I_NIVEL")
                .HasColumnType("int");

            builder.Property(t => t.I_ORDEN)
                .HasColumnName("I_ORDEN")
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
            public const string Name = "Menu";
        }

        public struct Columns
        {
            public const string I_ID_MENU = "I_ID_MENU";
            public const string I_ID_APLICACION = "I_ID_APLICACION";
            public const string V_MENU = "V_MENU";
            public const string V_DESCRIPCION = "V_DESCRIPCION";
            public const string V_ICONO = "V_ICONO";
            public const string V_RUTA = "V_RUTA";
            public const string V_URL = "V_URL";
            public const string V_NIVEL_PARENTESCO = "V_NIVEL_PARENTESCO";
            public const string V_PARENTESCO = "V_PARENTESCO";
            public const string I_NIVEL = "I_NIVEL";
            public const string I_ORDEN = "I_ORDEN";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
