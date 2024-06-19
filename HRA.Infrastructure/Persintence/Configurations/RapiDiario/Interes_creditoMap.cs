using HRA.Domain.Entities.RapiDiario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.RapiDiario
{
    public class Interes_creditoMap : IEntityTypeConfiguration<Interes_credito>
    {
        public void Configure(EntityTypeBuilder<Interes_credito> builder)
        {
            //Key
            builder.HasKey(e => e.I_ID_INTERES_CREDITO);

            //Table
            builder.ToTable("interes_credito", "rapidiario");

            //Properties
            builder.Property(e => e.I_ID_INTERES_CREDITO)
                .IsRequired()
                .HasColumnName("I_ID_INTERES_CREDITO")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.V_NOMBRE)
                .IsRequired()
                .HasColumnName("V_NOMBRE")
                .HasColumnType("varchar(20)")
                .HasMaxLength(20);

            builder.Property(e => e.I_TASA_INTERES)
                .IsRequired()
                .HasColumnName("I_TASA_INTERES")
                .HasColumnType("decimal(4, 2)");

            builder.Property(e => e.V_FRECUENCIA)
               .IsRequired()
               .HasColumnName("V_FRECUENCIA")
               .HasColumnType("varchar(20)")
               .HasMaxLength(20);

            builder.Property(e => e.V_DESCRIPCION)
                .HasColumnName("V_DESCRIPCION")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(e => e.B_ESTADO)
                .IsRequired()
                .HasColumnName("B_ESTADO")
                .HasColumnType("char(1)")
                .HasMaxLength(1);

            builder.Property(e => e.I_USUARIO_CREACION)
                .HasColumnName("I_USUARIO_CREACION")
                .HasColumnType("int");

            builder.Property(e => e.D_FECHA_CREACION)
                .HasColumnName("D_FECHA_CREACION")
                .HasColumnType("datetime");

            builder.Property(e => e.I_USUARIO_MODIFICA)
                .HasColumnName("I_USUARIO_MODIFICA")
                .HasColumnType("int");

            builder.Property(e => e.D_FECHA_MODIFICA)
                .HasColumnName("D_FECHA_MODIFICA")
                .HasColumnType("datetime");
        }
        public struct Table
        {
            public const string Schema = "rapidiario";
            public const string Name = "interes_credito";
        }
        public struct Columns
        {
            public const string I_ID_INTERES_CREDITO = "I_ID_INTERES_CREDITO";
            public const string V_NOMBRE = "V_NOMBRE";
            public const string I_TASA_INTERES = "I_TASA_INTERES";
            public const string V_FRECUENCIA = "V_FRECUENCIA";
            public const string V_DESCRIPCION = "V_DESCRIPCION";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
