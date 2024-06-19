using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.RapiDiario;

namespace HRA.Application.UseCases.Cuota_.Queries.ObtenerCuotas
{
    public record class CuotaDTO : IMapFrom<Cuota>
    {
        public int I_ID_INSTALLMENT { get; set; }
        public int I_CREDIT_ID { get; set; }
        public string V_INSTALLMENT_NUMBER { get; set; }
        public decimal I_INSTALLMENT_AMOUNT { get; set; }
        public decimal I_PRINCIPAL { get; set; }
        public decimal I_BALANCE_INICIAL { get; set; }
        public decimal I_INTEREST { get; set; }
        public decimal I_BALANCE_END { get; set; }
        public DateTime D_PAYMENT_DATE { get; set; }
        public string B_STATE { get; set; }
        public int I_USER_CREATE { get; set; }
        public int I_USER_MODIF { get; set; }
        public DateTime D_CREATE_DATE { get; set; }
        public DateTime D_MODIF_DATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Cuota, CuotaDTO>()
                .ForMember(dto => dto.I_ID_INSTALLMENT, et => et.MapFrom(a => a.I_ID_CUOTA))
                .ForMember(dto => dto.I_CREDIT_ID, et => et.MapFrom(a => a.I_ID_CREDITO))
                .ForMember(dto => dto.V_INSTALLMENT_NUMBER, et => et.MapFrom(a => a.V_NUMERO_CUOTA))
                .ForMember(dto => dto.I_INSTALLMENT_AMOUNT, et => et.MapFrom(a => a.I_MONTO_CUOTA))
                .ForMember(dto => dto.I_PRINCIPAL, et => et.MapFrom(a => a.I_CAPITAL))
                .ForMember(dto => dto.I_BALANCE_INICIAL, et => et.MapFrom(a => a.I_SALDO_INICIAL))
                .ForMember(dto => dto.I_INTEREST, et => et.MapFrom(a => a.I_INTERES))
                .ForMember(dto => dto.I_BALANCE_END, et => et.MapFrom(a => a.I_SALDO_FINAL))
                .ForMember(dto => dto.D_PAYMENT_DATE, et => et.MapFrom(a => a.D_FECHA_PAGO))
                .ForMember(dto => dto.I_USER_CREATE, et => et.MapFrom(a => a.I_USUARIO_CREACION))
                .ForMember(dto => dto.D_CREATE_DATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
                .ForMember(dto => dto.I_USER_MODIF, et => et.MapFrom(a => a.I_USUARIO_MODIFICA))
                .ForMember(dto => dto.D_MODIF_DATE, et => et.MapFrom(a => a.D_FECHA_MODIFICA))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }

    }
}
