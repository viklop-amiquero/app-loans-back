using HRA.Domain.Entities.RapiDiario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Rapidiario
{
    public class Tipo_cuentaMap : IEntityTypeConfiguration<Tipo_cuenta>
    {
        public void Configure(EntityTypeBuilder<Tipo_cuenta> builder)
        {
            // table
            builder.ToTable("tipo_cuenta", "rapidiario");

            // key
            builder.HasKey(t => t.I_ID_TIPO_CUENTA);

            //Properties
            builder.Property(t => t.I_ID_TIPO_CUENTA)
                .IsRequired()
                .HasColumnName("I_ID_TIPO_CUENTA")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.V_TIPO_CUENTA)
                .IsRequired()
                .HasColumnName("V_TIPO_CUENTA")
                .HasColumnType("varchar(20)")
                .HasMaxLength(20);

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
            public const string Name = "tipo_cuenta";
        }

        public struct Columns
        {
            public const string I_ID_TIPO_CUENTA = "I_ID_TIPO_CUENTA";
            public const string V_NUMERO_CUENTA = "V_NUMERO_CUENTA";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
