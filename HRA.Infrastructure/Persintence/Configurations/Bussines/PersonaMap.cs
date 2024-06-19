using HRA.Domain.Entities.Bussines;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Bussines
{
    public class PersonaMap : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            // table
            builder.ToTable("Persona", "dbo");

            // key
            builder.HasKey(t => t.I_ID_PERSONA);

            //Properties
            builder.Property(t => t.I_ID_PERSONA)
                .IsRequired()
                .HasColumnName("I_ID_PERSONA")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.I_ID_UBIGEO)
                .IsRequired()
                .HasColumnName("I_ID_UBIGEO")
                .HasColumnType("int");

            builder.Property(t => t.I_ID_SEXO)
                .IsRequired()
                .HasColumnName("I_ID_SEXO")
                .HasColumnType("int");

            builder.Property(t => t.V_PRIMER_NOMBRE)
                .IsRequired()
                .HasColumnName("V_PRIMER_NOMBRE")
                .HasColumnType("varchar(25)")
                .HasMaxLength(25);

            builder.Property(t => t.V_SEGUNDO_NOMBRE)
                .HasColumnName("V_SEGUNDO_NOMBRE")
                .HasColumnType("varchar(25)")
                .HasMaxLength(25);

            builder.Property(t => t.V_APELLIDO_PATERNO)
               .IsRequired()
               .HasColumnName("V_APELLIDO_PATERNO")
               .HasColumnType("varchar(50)")
               .HasMaxLength(50);

            builder.Property(t => t.V_APELLIDO_MATERNO)
               .IsRequired()
               .HasColumnName("V_APELLIDO_MATERNO")
               .HasColumnType("varchar(50)")
               .HasMaxLength(50);

            builder.Property(t => t.I_EDAD)
                .IsRequired()
                .HasColumnName("I_EDAD")
                .HasColumnType("int");

            builder.Property(t => t.D_FECHA_NACIMIENTO)
                .IsRequired()
                .HasColumnName("D_FECHA_NACIMIENTO")
                .HasColumnType("date");

            builder.Property(t => t.V_DIRECCION_DOMICILIO)
               .IsRequired()
               .HasColumnName("V_DIRECCION_DOMICILIO")
               .HasColumnType("varchar(100)")
               .HasMaxLength(100);

            builder.Property(t => t.V_DIRECCION_TRABAJO)
               .HasColumnName("V_DIRECCION_TRABAJO")
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
            public const string Schema = "dbo";
            public const string Name = "Persona";
        }

        public struct Columns
        {
            public const string I_ID_PERSONA = "I_ID_PERSONA";
            public const string I_ID_UBIGEO = "I_ID_UBIGEO";
            public const string I_ID_SEXO = "I_ID_SEXO";
            public const string V_PRIMER_NOMBRE = "V_PRIMER_NOMBRE";
            public const string V_SEGUNDO_NOMBRE = "V_SEGUNDO_NOMBRE";
            public const string V_APELLIDO_PATERNO = "V_APELLIDO_PATERNO";
            public const string V_APELLIDO_MATERNO = "V_APELLIDO_MATERNO";
            public const string I_EDAD = "I_EDAD";
            public const string D_FECHA_NACIMIENTO = "D_FECHA_NACIMIENTO";
            public const string V_DIRECCION_DOMICILIO = "V_DIRECCION_DOMICILIO";
            public const string V_DIRECCION_TRABAJO = "V_DIRECCION_TRABAJO";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
           
        }
    }
}
