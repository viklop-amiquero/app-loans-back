using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.RapiDiario;

namespace HRA.Application.UseCases.Operacion_.Queries.ObtenerOperacion
{
    public record class OperacionDTO: IMapFrom<Operacion>
    {
        public int I_ID_OPERATION { get; set; }
        public int I_ID_ACCOUNT { get; set; }
        public int? I_ID_INSTALLMENT { get; set; }
        public string V_ID_TYPE_OPERATION { get; set; }
        public string V_NUMBER_OPERATION { get; set; }
        public decimal I_AMOUNT { get; set; }
        public string? B_STATE { get; set; }
        public int I_USER_CREATE { get; set; }
        public int I_USER_MODIF { get; set; }
        public DateTime D_CREATE_DATE { get; set; }
        public DateTime D_MODIF_DATE { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Operacion, OperacionDTO>()
                .ForMember(dto => dto.I_ID_OPERATION, et => et.MapFrom(a => a.I_ID_OPERACION))
                .ForMember(dto => dto.I_ID_ACCOUNT, et => et.MapFrom(a => a.I_ID_CUENTA))
                .ForMember(dto => dto.I_ID_INSTALLMENT, et => et.MapFrom(a => a.I_ID_CUOTA))
                .ForMember(dto => dto.V_ID_TYPE_OPERATION, et => et.MapFrom(a => a.I_ID_TIPO_OPERACION))
                .ForMember(dto => dto.V_NUMBER_OPERATION, et => et.MapFrom(a => a.V_NUMERO_OPERACION))
                .ForMember(dto => dto.I_AMOUNT, et => et.MapFrom(a => a.I_MONTO))
                .ForMember(dto => dto.I_USER_CREATE, et => et.MapFrom(a => a.I_USUARIO_CREACION))
                .ForMember(dto => dto.D_CREATE_DATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
                .ForMember(dto => dto.I_USER_MODIF, et => et.MapFrom(a => a.I_USUARIO_MODIFICA))
                .ForMember(dto => dto.D_MODIF_DATE, et => et.MapFrom(a => a.D_FECHA_MODIFICA))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
