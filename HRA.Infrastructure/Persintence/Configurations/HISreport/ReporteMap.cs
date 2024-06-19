using HRA.Domain.Entities;
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
    //public class ReporteMap : IEntityTypeConfiguration<Reporte>
    //{
    //    public void Configure(EntityTypeBuilder<Reporte> builder)
    //    {
    //        // table
    //        builder.ToTable("reporte", "hisreport");

    //        // key
    //        builder.HasKey(t => t.ID_ID_REPORTE);

    //        //Properties
    //        builder.Property(t => t.ID_ID_REPORTE)
    //            .IsRequired()
    //            .HasColumnName("ID_ID_REPORTE")
    //            .HasColumnType("int")
    //            .ValueGeneratedOnAdd();

    //        builder.Property(t => t.V_LOGIN)
    //            .IsRequired()
    //            .HasColumnName("V_LOGIN")
    //            .HasColumnType("varchar(13)")
    //            .HasMaxLength(13);

    //        builder.Property(t => t.V_ID_ROL)
    //            .IsRequired()
    //            .HasColumnName("V_ID_ROL")
    //            .HasColumnType("varchar(150)")
    //            .HasMaxLength(150);

    //        builder.Property(t => t.V_IP)
    //            .IsRequired()
    //            .HasColumnName("V_IP")
    //            .HasColumnType("varchar(13)")
    //            .HasMaxLength(20);

    //        builder.Property(t => t.I_INTENTO)
    //            .IsRequired()
    //            .HasColumnName("I_INTENTO")
    //            .HasColumnType("int");

    //        builder.Property(t => t.I_ID_USUARIO)
    //            .HasColumnName("I_ID_USUARIO")
    //            .HasColumnType("int");

    //        builder.Property(t => t.D_FECHA_REGISTRO)
    //            .HasColumnName("D_FECHA_REGISTRO")
    //            .HasColumnType("datetime");
    //    }

    //    public struct Table
    //    {
    //        public const string Schema = "seguridad";
    //        public const string Name = "login";
    //    }

    //    public struct Columns
    //    {
    //        public const string I_ID_LOGIN = "I_ID_LOGIN";
    //        public const string V_LOGIN = "V_LOGIN";
    //        public const string V_ID_ROL = "V_ID_ROL";
    //        public const string V_IP = "V_IP";
    //        public const string I_INTENTO = "I_INTENTO";
    //        public const string I_ID_USUARIO = "I_ID_USUARIO";
    //        public const string D_FECHA_REGISTRO = "D_FECHA_REGISTRO";
    //    }
    //}
}
