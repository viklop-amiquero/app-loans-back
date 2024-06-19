using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.EntitiesStoreProcedure.SP_Credito;

namespace HRA.Application.UseCases.Credito_.Queries.Listado_creditos
{
    public record class ListadoCreditosDTO : IMapFrom<entity_listado_credito>
    {

        public int I_ID_CREDIT { get; set; }
        public string V_ACCOUNT { get; set; }
        public int I_ACCOUNT_ID { get; set; }
        public string V_TYPE_ACCOUNT { get; set; }
        public string V_TYPE_CREDIT { get; set; }
        public decimal I_AMOUNT_LOAN { get; set; }
        public string V_PAY_FREQUENCY { get; set; }
        public int I_TERM_QUANTITY { get; set; }
        public decimal I_INTEREST_RATE { get; set; }
        public DateTime D_DISBURSEMENT_DATE { get; set; }
        public decimal I_FINANCIAL_EXPENSE { get; set; }
        public decimal I_ACTUAL_AMOUNT { get; set; }
        public DateTime D_DATE_CREATE { get; set; }
        public string B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<entity_listado_credito, ListadoCreditosDTO>()
               .ForMember(dto => dto.I_ID_CREDIT, et => et.MapFrom(a => a.I_ID))
               .ForMember(dto => dto.V_ACCOUNT, et => et.MapFrom(a => a.V_CUENTA))
               .ForMember(dto => dto.I_ACCOUNT_ID, et => et.MapFrom(a => a.I_ID_CUENTA))
               .ForMember(dto => dto.V_TYPE_ACCOUNT, et => et.MapFrom(a => a.V_TIPO_CUENTA))
               .ForMember(dto => dto.V_TYPE_CREDIT, et => et.MapFrom(a => a.V_TIPO_CREDITO))
               .ForMember(dto => dto.I_AMOUNT_LOAN, et => et.MapFrom(a => a.I_MONTO_PRESTAMO))
               .ForMember(dto => dto.V_PAY_FREQUENCY, et => et.MapFrom(a => a.V_FRECUENCIA_PAGO))
               .ForMember(dto => dto.I_TERM_QUANTITY, et => et.MapFrom(a => a.I_PLAZO_CANTIDAD))
               .ForMember(dto => dto.I_INTEREST_RATE, et => et.MapFrom(a => a.I_TASA_INTERES))
               .ForMember(dto => dto.D_DISBURSEMENT_DATE, et => et.MapFrom(a => a.D_FECHA_DESEMBOLSO))
               .ForMember(dto => dto.D_DATE_CREATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
               .ForMember(dto => dto.I_FINANCIAL_EXPENSE, et => et.MapFrom(a => a.I_GASTO_FINANCIERO))
               .ForMember(dto => dto.I_ACTUAL_AMOUNT, et => et.MapFrom(a => a.I_MONTO_REAL))
               .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
   
    }
}
