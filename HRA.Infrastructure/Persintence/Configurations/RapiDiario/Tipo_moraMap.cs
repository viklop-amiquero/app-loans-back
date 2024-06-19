using HRA.Domain.Entities.RapiDiario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Rapidiario
{
    public class Tipo_moraMap : IEntityTypeConfiguration<Tipo_mora>
    {
        public void Configure(EntityTypeBuilder<Tipo_mora> builder)
        {
            //table
            builder.ToTable("tipo_mora", "rapidiario");

            //key
            builder.HasKey(e => e.I_ID_TIPO_MORA);

            //properties
            builder.Property(e => e.I_ID_TIPO_MORA)
                .IsRequired()
                .HasColumnName("I_ID_TIPO_MORA")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.V_NOMBRE)
                .IsRequired()
                .HasColumnName("V_NOMBRE")
                .HasColumnType("varchar(20)")
                .HasMaxLength(20);

            builder.Property(e => e.I_MONTO)
                .IsRequired()
                .HasColumnName("I_MONTO")
                .HasColumnType("decimal(4, 2)");

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
            public const string Shema = "rapidiario";
            public const string Name = "tipo_mora";
        }
        public struct Columns
        {
            public const string I_ID_TIPO_MORA = "I_ID_TIPO_MORA";
            public const string V_NOMBRE = "V_NOMBRE";
            public const string I_MONTO = "I_MONTO";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
            

            

           
        
    }
}
