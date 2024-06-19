using HRA.Domain.Entities.RapiDiario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.Rapidiario
{
    public class Tipo_operacionMap : IEntityTypeConfiguration<Tipo_operacion>
    {
        public void Configure(EntityTypeBuilder<Tipo_operacion> builder)
        {
            //Table
            builder.ToTable("tipo_operacion", "rapidiario");

            //Key
            builder.HasKey(e => e.I_ID_TIPO_OPERACION);

            //Properties
            builder.Property(e => e.I_ID_TIPO_OPERACION)
                .IsRequired()
                .HasColumnName("I_ID_TIPO_OPERACION")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.V_IDENTIFICADOR)
                .IsRequired()
                .HasColumnName("V_IDENTIFICADOR")
                .HasColumnType("varchar(20)")
                .HasMaxLength(20);

            builder.Property(e => e.V_TIPO_OPERACION)
                .IsRequired()
                .HasColumnName("V_TIPO_OPERACION")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(e => e.V_DESCRIPCION)
                .HasColumnName("V_DESCRIPCION")
                .HasColumnType("varchar(150)")
                .HasMaxLength(150);

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
            public const string Name = "Tipo_operacion";
        }
        public struct Columns
        {
            public const string I_ID_TIPO_OPERACION = "I_ID_TIPO_OPERACION";
            public const string V_IDENTIFICADOR = "V_IDENTIFICADOR";
            public const string V_TIPO_OPERACION = "V_TIPO_OPERACION";
            public const string V_DESCRIPCION = "V_DESCRIPCION";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
