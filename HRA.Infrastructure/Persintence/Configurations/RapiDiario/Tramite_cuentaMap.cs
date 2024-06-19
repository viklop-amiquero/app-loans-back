﻿using HRA.Domain.Entities.RapiDiario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRA.Infrastructure.Persintence.Configurations.RapiDiario
{
    public class Tramite_cuentaMap : IEntityTypeConfiguration<Tramite_cuenta>
    {
        public void Configure(EntityTypeBuilder<Tramite_cuenta> builder)
        {
            // table
            builder.ToTable("tramite_cuenta", "rapidiario");

            // key
            builder.HasKey(t => t.I_ID_TRAMITE_CUENTA);

            //Properties
            builder.Property(t => t.I_ID_TRAMITE_CUENTA)
                .IsRequired()
                .HasColumnName("I_ID_TRAMITE_CUENTA")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.I_ID_CUENTA)
                .IsRequired()
                .HasColumnName("I_ID_CUENTA")
                .HasColumnType("int");

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
            public const string I_ID_TRAMITE_CUENTA = "I_ID_TRAMITE_CUENTA";
            public const string I_ID_TRAMITE_DOC = "I_ID_TRAMITE_DOC";
            public const string I_ID_CUENTA = "I_ID_CUENTA";
            public const string B_ESTADO = "B_ESTADO";
            public const string I_USUARIO_CREACION = "I_USUARIO_CREACION";
            public const string D_FECHA_CREACION = "D_FECHA_CREACION";
            public const string I_USUARIO_MODIFICA = "I_USUARIO_MODIFICA";
            public const string D_FECHA_MODIFICA = "D_FECHA_MODIFICA";
        }
    }
}
