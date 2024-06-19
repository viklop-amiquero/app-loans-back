using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.EntitiesStoreProcedure.SP_Cuota;

namespace HRA.Application.UseCases.Cuota_.Queries.Listado_cuotas
{
    public record class ListadoCuotasDTO : IMapFrom<entity_listado_cuota>
    {
        public int I_ID_INSTALLMENT { get; set; }
        public int I_ID_ACCOUNT { get; set; }
        public int I_CREDIT_ID { get; set; }
        public string V_INSTALLMENT_NUMBER { get; set; }
        public decimal I_INSTALLMENT_AMOUNT { get; set; }
        public decimal I_PRINCIPAL { get; set; }
        public decimal I_BALANCE_INICIAL { get; set; }
        public decimal I_INTEREST { get; set; }
        public decimal I_BALANCE_END { get; set; }
        public DateTime D_PAYMENT_DATE { get; set; }
        public DateTime? D_CREATE_DATE { get; set; }
        public decimal? I_TOTAL_AMOUNT { get; set; }
        public string? formattedDate { get; set; }
        public string? B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<entity_listado_cuota, ListadoCuotasDTO>()
               .ForMember(dto => dto.I_ID_INSTALLMENT, et => et.MapFrom(a => a.I_ID))
               .ForMember(dto => dto.I_ID_ACCOUNT, et => et.MapFrom(a => a.I_ID_CUENTA))
               .ForMember(dto => dto.I_CREDIT_ID, et => et.MapFrom(a => a.I_ID_CREDITO))
               .ForMember(dto => dto.V_INSTALLMENT_NUMBER, et => et.MapFrom(a => a.V_NUMERO_CUOTA))
               .ForMember(dto => dto.I_INSTALLMENT_AMOUNT, et => et.MapFrom(a => a.I_MONTO_CUOTA))
               .ForMember(dto => dto.I_PRINCIPAL, et => et.MapFrom(a => a.I_CAPITAL))
               .ForMember(dto => dto.I_BALANCE_INICIAL, et => et.MapFrom(a => a.I_SALDO_INICIAL))
               .ForMember(dto => dto.I_INTEREST, et => et.MapFrom(a => a.I_INTERES))
               .ForMember(dto => dto.I_BALANCE_END, et => et.MapFrom(a => a.I_SALDO_FINAL))
               .ForMember(dto => dto.D_PAYMENT_DATE, et => et.MapFrom(a => a.D_FECHA_PAGO))
               .ForMember(dto => dto.D_CREATE_DATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
               .ForMember(dto => dto.I_TOTAL_AMOUNT, et => et.MapFrom(a => a.I_MONTO_TOTAL))
               .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO))
               .ForMember(dto => dto.formattedDate, opt => opt.MapFrom(a => a.D_FECHA_PAGO != null ? a.D_FECHA_PAGO.Value.ToString("dd/MM/yyyy") : ""));
        }

    }
}
