using HRA.Domain.Entities.Operaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Operaciones
{
    public class Contacto_emergenciaMap : IEntityTypeConfiguration<Contacto_emergencia>
    {
        public void Configure(EntityTypeBuilder<Contacto_emergencia> builder)
        {
            // table
            builder.ToTable("contacto_emergencia", "operaciones");

            // key
            builder.HasKey(t => t.I_ID_CONTACTO_EM);

            //Properties
            builder.Property(t => t.I_ID_CONTACTO_EM)
                .IsRequired()
                .HasColumnName("I_ID_CONTACTO_EM")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.I_ID_PERSONA)
                .IsRequired()
                .HasColumnName("I_ID_PERSONA")
                .HasColumnType("int");

            builder.Property(t => t.V_NOMBRE)
                .HasColumnName("V_NOMBRE")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.V_PARENTESCO)
               .HasColumnName("V_PARENTESCO")
               .HasColumnType("varchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.V_TELEFONO)
                .HasColumnName("V_TELEFONO")
                .HasColumnType("varchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.V_CELULAR)
                .HasColumnName("V_CELULAR")
                .HasColumnType("varchar(9)")
                .HasMaxLength(9);

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
            public const string Name = "contacto_emergencia";
        }

        public struct Columns
        {
            public const string I_ID_CONTACTO_EM = "I_ID_CONTACTO_EM";
            public const string I_ID_PERSONA = "I_ID_PERSONA";
            public const string V_PARENTESCO = "V_PARENTESCO";
            public const string V_NOMBRE = "V_NOMBRE";
            public const string V_CELULAR = "V_CELULAR";
            public const string V_TELEFONO = "V_TELEFONO";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    
    }
}
