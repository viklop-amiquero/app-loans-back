using HRA.Domain.Entities.RapiDiario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Rapidiario
{
    public class MoraMap : IEntityTypeConfiguration<Mora>
    {
        public void Configure(EntityTypeBuilder<Mora> builder)
        {
            //Table
            builder.ToTable("mora", "rapidiario");

            //key
            builder.HasKey(e => e.I_ID_MORA);

            //properties
            builder.Property(e => e.I_ID_MORA)
                .IsRequired()
                .HasColumnName("I_ID_MORA")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.I_ID_CUOTA)
                .IsRequired()
                .HasColumnName("I_ID_CUOTA")
                .HasColumnType("int");

            builder.Property(e => e.I_ID_TIPO_MORA)
                .IsRequired()
                .HasColumnName("I_ID_TIPO_MORA")
                .HasColumnType("int");

            builder.Property(e => e.I_MONTO_MORA)
                .IsRequired()
                .HasColumnName("I_MONTO_MORA")
                .HasColumnType("decimal(4, 2)");

            builder.Property(e => e.I_NUMERO_DIA)
                .IsRequired()
                .HasColumnName("I_NUMERO_DIA")
                .HasColumnType("int");

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
            public const string Name = "Mora";
        }
        public struct Columns
        {
            public const string I_ID_MORA = "I_ID_MORA";
            public const string I_ID_CUOTA = "I_ID_CUOTA";
            public const string I_ID_TIPO_MORA = "I_ID_TIPO_MORA";
            public const string I_MONTO_MORA = "I_MONTO_MORA";
            public const string I_NUMERO_DIA = "I_NUMERO_DIA";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
