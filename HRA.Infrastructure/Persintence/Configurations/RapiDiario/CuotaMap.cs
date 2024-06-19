using HRA.Domain.Entities.RapiDiario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.RapiDiario
{
    public class CuotaMap : IEntityTypeConfiguration<Cuota>
    {
        public void Configure(EntityTypeBuilder<Cuota> builder)
        {
            // table
            builder.ToTable("cuota", "rapidiario");

            // key
            builder.HasKey(t => t.I_ID_CUOTA);

            //Properties
            builder.Property(t => t.I_ID_CUOTA)
                .IsRequired()
                .HasColumnName("I_ID_CUOTA")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.I_ID_CREDITO)
                .IsRequired()
                .HasColumnName("I_ID_CREDITO")
                .HasColumnType("int");

            builder.Property(t => t.V_NUMERO_CUOTA)
                //.IsRequired()
                .HasColumnName("V_NUMERO_CUOTA")
                .HasColumnType("varchar(5)")
                .HasMaxLength(5);

            builder.Property(t => t.I_MONTO_CUOTA)
               .IsRequired()
               .HasColumnName("I_MONTO_CUOTA")
               .HasColumnType("decimal(7,2)");

            builder.Property(t => t.I_CAPITAL)
               .IsRequired()
               .HasColumnName("I_CAPITAL")
               .HasColumnType("decimal(10,2)");

            builder.Property(t => t.I_SALDO_INICIAL)
               .IsRequired()
               .HasColumnName("I_SALDO_INICIAL")
               .HasColumnType("decimal(10,2)");

            builder.Property(t => t.I_INTERES)
               .IsRequired()
               .HasColumnName("I_INTERES")
               .HasColumnType("decimal(6,2)");

            builder.Property(t => t.I_SALDO_FINAL)
              .IsRequired()
              .HasColumnName("I_SALDO_FINAL")
              .HasColumnType("decimal(10,2)");

            builder.Property(t => t.D_FECHA_PAGO)
                .IsRequired()
                .HasColumnName("D_FECHA_PAGO")
                .HasColumnType("datetime");

            builder.Property(t => t.I_MONTO_TOTAL)
               .IsRequired()
               .HasColumnName("I_MONTO_TOTAL")
               .HasColumnType("decimal(10,2)");

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
            public const string Name = "cuota";
        }

        public struct Columns
        {
            public const string I_ID_CUOTA = "I_ID_CUOTA";
            public const string I_ID_CREDITO = "I_ID_CREDITO";
            public const string V_NUMERO_CUOTA = "V_NUMERO_CUOTA";
            public const string I_MONTO_CUOTA = "I_MONTO_CUOTA";
            public const string I_CAPITAL = "I_CAPITAL";
            public const string I_SALDO_INICIAL = "I_SALDO_INICIAL";
            public const string I_INTERES = "I_INTERES";
            public const string I_SALDO_FINAL = "I_SALDO_FINAL";
            public const string D_FECHA_PAGO = "D_FECHA_PAGO";
            public const string I_MONTO_TOTAL = "I_MONTO_TOTAL";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
