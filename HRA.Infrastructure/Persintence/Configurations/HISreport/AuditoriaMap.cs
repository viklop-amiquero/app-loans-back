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
    public class AuditoriaMap : IEntityTypeConfiguration<Auditoria>
    {
        void IEntityTypeConfiguration<Auditoria>.Configure(EntityTypeBuilder<Auditoria> builder)
        {
            // table
            builder.ToTable("auditoria", "hisreport");

            // key
            builder.HasKey(t => t.I_ID_AUDITORIA);

            //Properties
            builder.Property(t => t.I_ID_AUDITORIA)
                .IsRequired()
                .HasColumnName("I_ID_AUDITORIA")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();


            builder.Property(t => t.I_ID_USER)
                .IsRequired()
                .HasColumnName("I_ID_USER")
                .HasColumnType("int");



            builder.Property(t => t.I_ID_REPORTE)
                .IsRequired()
                .HasColumnName("I_ID_REPORTE")
                .HasColumnType("int)");



            builder.Property(t => t.V_PC)
                .IsRequired()
                .HasColumnName("V_PC")
                .HasColumnType("varchar(50)");
               


            builder.Property(t => t.V_OBSERVACION)
                .IsRequired()
                .HasColumnName("V_OBSERVACION")
                .HasColumnType("varchar(300)")
                .HasMaxLength(300);


            builder.Property(t => t.D_FECHA)
                .IsRequired()
                .HasColumnName("D_FECHA")
                .HasColumnType("datetime");                

      
        }
        public struct Table
        {
            public const string Schema = "hisreport";
            public const string Name = "auditoria";
        }

        public struct Columns
        {
            public const string I_ID_AUDITORIA = "I_ID_AUDITORIA";
            public const string I_ID_USER = "I_ID_USER";
            public const string I_ID_REPORTE = "I_ID_REPORTE";
            public const string V_PC = "V_PC";
            public const string V_OBSERVACION = "V_OBSERVACION";
            public const string D_FECHA = "D_FECHA";
        }
            
        }
        
}
