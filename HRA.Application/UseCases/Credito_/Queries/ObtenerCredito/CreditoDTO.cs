using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.RapiDiario;

namespace HRA.Application.UseCases.Credito_.Queries.ObtenerCredito
{
    public record class CreditoDTO: IMapFrom<Credito>
    {
        public int I_ID_CREDIT { get; set; }
        public int I_ID_ACCOUNT { get; set; }
        public int I_TYPE_CREDIT_ID { get; set; }
        public int I_ID_PAYMENT_FREQUENCY { get; set; }
        public int I_INTEREST_CREDIT_ID { get; set; }
        public decimal I_LOAN_AMOUNT { get; set; }
        public int I_TERM_QUANTITY { get; set; }
        public DateTime D_DISBURSEMENT_DATE { get; set; }
        public decimal I_FINANCIAL_EXPENSE { get; set; }
        public decimal I_ACTUAL_AMOUNT { get; set; }
        public string? B_STATE { get; set; }
        public int I_USER_CREATE { get; set; }
        public int I_USER_MODIF { get; set; }
        public DateTime D_CREATE_DATE { get; set; }
        public DateTime D_MODIF_DATE { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Credito, CreditoDTO>()
                .ForMember(dto => dto.I_ID_CREDIT, et => et.MapFrom(a => a.I_ID_CREDITO))
                .ForMember(dto => dto.I_ID_ACCOUNT, et => et.MapFrom(a => a.I_ID_CUENTA))
                .ForMember(dto => dto.I_TYPE_CREDIT_ID, et => et.MapFrom(a => a.I_ID_TIPO_CREDITO))
                .ForMember(dto => dto.I_ID_PAYMENT_FREQUENCY, et => et.MapFrom(a => a.I_ID_FREC_PAGO))
                .ForMember(dto => dto.I_INTEREST_CREDIT_ID, et => et.MapFrom(a => a.I_ID_INTERES_CREDITO))
                .ForMember(dto => dto.I_LOAN_AMOUNT, et => et.MapFrom(a => a.I_MONTO_PRESTAMO))
                .ForMember(dto => dto.I_TERM_QUANTITY, et => et.MapFrom(a => a.I_PLAZO_CANTIDAD))
                .ForMember(dto => dto.D_DISBURSEMENT_DATE, et => et.MapFrom(a => a.D_FECHA_DESEMBOLSO))
                .ForMember(dto => dto.I_USER_CREATE, et => et.MapFrom(a => a.I_USUARIO_CREACION))
                .ForMember(dto => dto.D_CREATE_DATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
                .ForMember(dto => dto.I_USER_MODIF, et => et.MapFrom(a => a.I_USUARIO_MODIFICA))
                .ForMember(dto => dto.D_MODIF_DATE, et => et.MapFrom(a => a.D_FECHA_MODIFICA))
                .ForMember(dto => dto.I_FINANCIAL_EXPENSE, et => et.MapFrom(a => a.I_GASTO_FINANCIERO))
                .ForMember(dto => dto.I_ACTUAL_AMOUNT, et => et.MapFrom(a => a.I_MONTO_REAL))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
