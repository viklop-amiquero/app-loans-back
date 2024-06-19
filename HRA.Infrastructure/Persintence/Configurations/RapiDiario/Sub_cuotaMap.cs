using HRA.Domain.Entities.RapiDiario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.RapiDiario
{
    public class Sub_cuotaMap : IEntityTypeConfiguration<Sub_cuota>
    {
        public void Configure(EntityTypeBuilder<Sub_cuota> builder)
        {
            // table
            builder.ToTable("sub_cuota", "rapidiario");

            // key
            builder.HasKey(t => t.I_ID_SUB_CUOTA);

            //Properties
            builder.Property(t => t.I_ID_SUB_CUOTA)
                .IsRequired()
                .HasColumnName("I_ID_SUB_CUOTA")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.I_ID_CUOTA)
                .IsRequired()
                .HasColumnName("I_ID_CUOTA")
                .HasColumnType("int");


            builder.Property(t => t.I_MONTO)
               .IsRequired()
               .HasColumnName("I_MONTO")
               .HasColumnType("decimal(10,2)");

            builder.Property(t => t.I_SALDO_CUOTA)
               .IsRequired()
               .HasColumnName("I_SALDO_CUOTA")
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
            public const string Name = "sub_cuota";
        }

        public struct Columns
        {
            public const string I_ID_SUB_CUOTA = "I_ID_SUB_CUOTA";
            public const string I_ID_CUOTA = "I_ID_CUOTA";
            public const string I_MONTO = "I_MONTO";
            public const string I_SALDO_CUOTA = "I_SALDO_CUOTA";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
