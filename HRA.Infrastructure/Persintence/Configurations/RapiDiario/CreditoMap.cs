using HRA.Domain.Entities.RapiDiario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.RapiDiario
{
    public class CreditoMap : IEntityTypeConfiguration<Credito>
    {
        public void Configure(EntityTypeBuilder<Credito> builder)
        {
            // table
            builder.ToTable("credito", "rapidiario");

            // key
            builder.HasKey(t => t.I_ID_CREDITO);

            //Properties
            builder.Property(t => t.I_ID_CREDITO)
                .IsRequired()
                .HasColumnName("I_ID_CREDITO")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.I_ID_CUENTA)
                .IsRequired()
                .HasColumnName("I_ID_CUENTA")
                .HasColumnType("int");

            builder.Property(t => t.I_ID_TIPO_CREDITO)
               .IsRequired()
               .HasColumnName("I_ID_TIPO_CREDITO")
               .HasColumnType("int");

            builder.Property(t => t.I_ID_FREC_PAGO)
               .IsRequired()
               .HasColumnName("I_ID_FREC_PAGO")
               .HasColumnType("int");

            builder.Property(t => t.I_ID_INTERES_CREDITO)
               .IsRequired()
               .HasColumnName("I_ID_INTERES_CREDITO")
               .HasColumnType("int");

            builder.Property(t => t.I_MONTO_PRESTAMO)
               .IsRequired()
               .HasColumnName("I_MONTO_PRESTAMO")
               .HasColumnType("decimal(10,2)");

            builder.Property(t => t.I_PLAZO_CANTIDAD)
               .IsRequired()
               .HasColumnName("I_PLAZO_CANTIDAD")
               .HasColumnType("int");

            builder.Property(t => t.D_FECHA_DESEMBOLSO)
                .IsRequired()
                .HasColumnName("D_FECHA_DESEMBOLSO")
                .HasColumnType("datetime");

            builder.Property(t => t.I_GASTO_FINANCIERO)
               .IsRequired()
               .HasColumnName("I_GASTO_FINANCIERO")
               .HasColumnType("decimal(10,2)");

            builder.Property(t => t.I_MONTO_REAL)
               .IsRequired()
               .HasColumnName("I_MONTO_REAL")
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
            public const string Name = "credito";
        }

        public struct Columns
        {
            public const string I_ID_CREDITO = "I_ID_CREDITO";
            public const string I_ID_CUENTA = "I_ID_CUENTA";
            public const string I_ID_TIPO_CREDITO = "I_ID_TIPO_CREDITO";
            public const string I_ID_FREC_PAGO = "I_ID_FREC_PAGO";
            public const string I_ID_INTERES_CREDITO = "I_ID_INTERES_CREDITO";
            public const string I_MONTO_PRESTAMO = "I_MONTO_PRESTAMO";
            public const string I_PLAZO_CANTIDAD = "I_PLAZO_CANTIDAD";
            public const string D_FECHA_DESEMBOLSO = "D_FECHA_DESEMBOLSO";
            public const string I_GASTO_FINANCIERO = "I_GASTO_FINANCIERO";
            public const string I_MONTO_REAL = "I_MONTO_REAL";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
