using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.EntitiesStoreProcedure.SP_Operacion;
using System.Globalization;

namespace HRA.Application.UseCases.Operacion_.Queries.Listado_operaciones
{
    public record class ListadoOperacionesDTO : IMapFrom<entity_listado_operacion>
    {

        public int I_ID_OPERATION { get; set; }
        public int I_ID_ACCOUNT { get; set; }
        public int? I_ID_INSTALLMENT { get; set; }
        public string V_TYPE_OPERATION { get; set; }
        public string V_NUMBER_OPERATION { get; set; }
        public decimal I_AMOUNT { get; set; }
        public string V_NUMBER_ACCOUNT { get; set; }
        public string? V_REVERSE { get; set; }
        public string? B_STATE { get; set; }
        public DateTime D_DATE_CREATE { get; set; }
        public string FormattedDate { get; set; }
        public string FormattedTime { get; set; }

        public string FormattedMonth { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<entity_listado_operacion, ListadoOperacionesDTO>()
               .ForMember(dto => dto.I_ID_OPERATION, et => et.MapFrom(a => a.I_ID_OPERACION))
               .ForMember(dto => dto.I_ID_ACCOUNT, et => et.MapFrom(a => a.I_ID_CUENTA))
               .ForMember(dto => dto.I_ID_INSTALLMENT, et => et.MapFrom(a => a.I_ID_CUOTA))
              .ForMember(dto => dto.V_TYPE_OPERATION, et => et.MapFrom(a => a.V_TIPO_OPERACION))
              .ForMember(dto => dto.V_NUMBER_OPERATION, et => et.MapFrom(a => a.V_NUMERO_OPERACION))
              .ForMember(dto => dto.I_AMOUNT, et => et.MapFrom(a => a.I_MONTO))
              .ForMember(dto => dto.V_NUMBER_ACCOUNT, et => et.MapFrom(a => a.V_NUMERO_CUENTA))
              .ForMember(dto => dto.V_REVERSE, et => et.MapFrom(a => a.V_REVERSION))
               .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO))
               .ForMember(dto => dto.D_DATE_CREATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
               .ForMember(dto => dto.FormattedDate, et => et.MapFrom(a => FormatDate(a.D_FECHA_CREACION)))
               .ForMember(dto => dto.FormattedTime, et => et.MapFrom(a => FormatTime(a.D_FECHA_CREACION)))
               .ForMember(dto => dto.FormattedMonth, et => et.MapFrom(a => GetMonthName(a.D_FECHA_CREACION)));

        }

        private string FormatDate(DateTime date)
        {
            TimeSpan timeDifference = DateTime.Now.Date - date.Date;

            if (timeDifference.Days == 0)
            {
                return "Hoy";
            }
            else if (timeDifference.Days == 1)
            {
                return "Ayer";
            }
            else
            {
                return date.ToString("dd/MM/yyyy");
            }
        }

        private string FormatTime(DateTime date)
        {
            return date.ToString("h:mm tt").ToUpper();
        }

        private string GetMonthName(DateTime date)
        {
            CultureInfo spanishCulture = new CultureInfo("es-ES");
            return date.ToString("MMMM", spanishCulture);
        }

    }
}
