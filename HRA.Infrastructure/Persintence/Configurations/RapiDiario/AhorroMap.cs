using HRA.Domain.Entities.RapiDiario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.RapiDiario
{
    public class AhorroMap : IEntityTypeConfiguration<Ahorro>
    {
        public void Configure(EntityTypeBuilder<Ahorro> builder)
        {
            //Key
            builder.HasKey(e => e.I_ID_AHORRO);

            //Table
            builder.ToTable("ahorro", "rapidiario");

            //Properties
            builder.Property(e => e.I_ID_AHORRO)
                .IsRequired()
                .HasColumnName("I_ID_AHORRO")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.I_ID_CUENTA)
                .IsRequired()
                .HasColumnName("I_ID_CUENTA")
                .HasColumnType("int");

            builder.Property(e => e.I_ID_INTERES_AHORRO)
                .IsRequired()
                .HasColumnName("I_ID_INTERES_AHORRO")
                .HasColumnType("int");

            builder.Property(e => e.V_TIPO_AHORRO)
               .HasColumnName("V_TIPO_AHORRO")
               .HasColumnType("varchar(20)")
               .HasMaxLength(20);

            builder.Property(e => e.I_MONTO_AHORRO)
                .HasColumnName("I_MONTO_AHORRO")
                .HasColumnType("decimal(10,2)");

            builder.Property(e => e.I_MESES)
                .HasColumnName("I_MESES")
                .HasColumnType("int");

            builder.Property(e => e.I_INSCRIPCION)
                .HasColumnName("I_INSCRIPCION")
                .HasColumnType("decimal(10, 2)");

            builder.Property(e => e.I_MONTO_ULTIMA_TRANSACCION)
                .HasColumnName("I_MONTO_ULTIMA_TRANSACCION")
                .HasColumnType("decimal(10, 2)");

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
            public const string Name = "ahorro";
        }
        public struct Columns
        {
            public const string I_ID_AHORRO = "I_ID_AHORRO";
            public const string I_ID_CUENTA = "I_ID_CUENTA";
            public const string I_ID_INTERES_AHORRO = "I_ID_INTERES_AHORRO";
            public const string V_TIPO_AHORRO = "V_TIPO_AHORRO";
            public const string I_MONTO_AHORRO = "I_MONTO_AHORRO";
            public const string I_MESES = "I_MESES";
            public const string I_INSCRIPCION = "I_INSCRIPCION";
            public const string I_MONTO_ULTIMA_TRANSACCION = "I_MONTO_ULTIMA_TRANSACCION";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
