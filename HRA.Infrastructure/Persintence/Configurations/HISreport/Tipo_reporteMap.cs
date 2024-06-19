using HRA.Domain.Entities.HISreport;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA.Infrastructure.Persintence.Configurations.HISreport
{
    public class Tipo_reporteMap : IEntityTypeConfiguration<Tipo_reporte>
    {
        public void Configure(EntityTypeBuilder<Tipo_reporte> builder)
        {

            // table
            builder.ToTable("tipo_reporte", "hisreport");

            // key
            builder.HasKey(t => t.I_ID_TIPO_REPORTE);

            //Properties
            builder.Property(t => t.I_ID_TIPO_REPORTE)
                .IsRequired()
                .HasColumnName("I_ID_TIPO_REPORTE")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.V_NOMBRE)
                .IsRequired()
                .HasColumnName("V_NOMBRE")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);
        }
        public struct Table
        {
            public const string Schema = "hisreport";
            public const string Name = "tipo_reporte";
        }

        public struct Columns
        {
            public const string I_ID_TIPO_REPORTE = "I_ID_TIPO_REPORTE";
            public const string V_NOMBRE = "V_NOMBRE";
        }
    }
}
