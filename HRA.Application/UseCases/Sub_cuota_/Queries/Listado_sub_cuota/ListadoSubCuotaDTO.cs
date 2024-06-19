using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.EntitiesStoreProcedure.SP_SubCuota;

namespace HRA.Application.UseCases.Sub_cuota_.Queries.Listado_cuota
{
    public record class ListadoSubCuotaDTO : IMapFrom<entity_listado_sub_cuota>
    {
        public int? I_ID_SUB_INSTALLMENT { get; set; }
        public int? I_ID_INSTALLMENT { get; set; }
        public decimal? I_AMOUNT { get; set; }
        public decimal? I_BALANCE_INSTALLMENT { get; set; }
        public DateTime? D_CREATE_DATE { get; set; }
        public string? formattedDate { get; set; }
        public string? B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<entity_listado_sub_cuota, ListadoSubCuotaDTO>()
               .ForMember(dto => dto.I_ID_INSTALLMENT, et => et.MapFrom(a => a.I_ID_SUB_CUOTA))
               .ForMember(dto => dto.I_ID_INSTALLMENT, et => et.MapFrom(a => a.I_ID_CUOTA))
               .ForMember(dto => dto.I_AMOUNT, et => et.MapFrom(a => a.I_MONTO))
               .ForMember(dto => dto.I_BALANCE_INSTALLMENT, et => et.MapFrom(a => a.I_SALDO_CUOTA))
               .ForMember(dto => dto.D_CREATE_DATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
               .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO))
                .ForMember(dto => dto.formattedDate, opt => opt.MapFrom(a => a.D_FECHA_CREACION != null ? a.D_FECHA_CREACION.Value.ToString("dd/MM/yyyy") : ""));
        }

    }
}
