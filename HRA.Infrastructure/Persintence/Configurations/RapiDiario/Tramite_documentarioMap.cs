using HRA.Domain.Entities.RapiDiario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.RapiDiario
{
    public class Tramite_documentarioMap : IEntityTypeConfiguration<Tramite_documentario>
    {
        public void Configure(EntityTypeBuilder<Tramite_documentario> builder)
        {
            //Table
            builder.ToTable("tramite_documentario", "rapidiario");

            //Key
            builder.HasKey(e => e.I_ID_TRAMITE_DOC);

            //Properties
            builder.Property(e => e.I_ID_TRAMITE_DOC)
                .IsRequired()
                .HasColumnName("I_ID_TRAMITE_DOC")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.V_NOMBRE)
                .IsRequired()
                .HasColumnName("V_NOMBRE")
                .HasMaxLength(50)
                .HasColumnType("varchar");

            builder.Property(e => e.I_TARIFA)
                .IsRequired()
                .HasColumnName("I_TARIFA")
                .HasColumnType("decimal");

            builder.Property(e => e.V_DESCRIPCION)
                .HasColumnName("V_DESCRIPCION")
                .HasColumnType("varchar")
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
            public const string Schema = "rapidiario";
            public const string Name = "tramite_documentario";
        }

        public struct Columns
        {
            public const string I_ID_TRAMITE_DOC = "I_ID_TRAMITE_DOC";
            public const string V_NOMBRE = "V_NOMBRE";
            public const string I_TARIFA = "I_TARIFA";
            public const string V_DESCRIPCION = "V_DESCRIPCION";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
