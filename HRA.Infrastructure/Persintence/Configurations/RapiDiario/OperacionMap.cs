using HRA.Domain.Entities.RapiDiario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.RapiDiario
{
    public class OperacionMap : IEntityTypeConfiguration<Operacion>
    {
        public void Configure(EntityTypeBuilder<Operacion> builder)
        {
            // table
            builder.ToTable("operacion", "rapidiario");

            // key
            builder.HasKey(t => t.I_ID_OPERACION);

            //Properties
            builder.Property(t => t.I_ID_OPERACION)
                .IsRequired()
                .HasColumnName("I_ID_OPERACION")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.I_ID_CUENTA)
                .IsRequired()
                .HasColumnName("I_ID_CUENTA")
                .HasColumnType("int");

            builder.Property(t => t.I_ID_CUOTA)
                .HasColumnName("I_ID_CUOTA")
                .HasColumnType("int");

            builder.Property(t => t.I_ID_TIPO_OPERACION)
               .IsRequired()
               .HasColumnName("I_ID_TIPO_OPERACION")
               .HasColumnType("int");

            builder.Property(t => t.V_NUMERO_OPERACION)
               .IsRequired()
               .HasColumnName("V_NUMERO_OPERACION")
               .HasColumnType("varchar(10)")
               .HasMaxLength(10);

            builder.Property(t => t.I_MONTO)
               .IsRequired()
               .HasColumnName("I_MONTO")
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
            public const string Name = "operacion";
        }

        public struct Columns
        {
            public const string I_ID_OPERACION = "I_ID_OPERACION";
            public const string I_ID_CUENTA = "I_ID_CUENTA";
            public const string I_ID_CUOTA = "I_ID_CUOTA";
            public const string I_ID_TIPO_OPERACION = "I_ID_TIPO_OPERACION";
            public const string V_NUMERO_OPERACION = "V_NUMERO_OPERACION";
            public const string I_MONTO = "I_MONTO";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
