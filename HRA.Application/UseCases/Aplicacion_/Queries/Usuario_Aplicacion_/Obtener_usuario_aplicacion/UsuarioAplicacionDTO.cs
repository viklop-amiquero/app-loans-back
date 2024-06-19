using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.Application;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Usuario_Aplicacion_.Obtener_usuario_aplicacion
{
    public record class UsuarioAplicacionDTO : IMapFrom<Usuario_Aplicacion>
    {
        public int I_USER_ID { get; set; }
        public int I_ROLE_ID { get; set; }
        public int I_PERSON_ID { get; set; }
        public string V_USER { get; set; } = string.Empty;
        public DateTime D_START_DATE { get; set; }
        public DateTime? D_END_DATE { get; set; }
        public string B_STATE { get; set; } = string.Empty;
        public int? I_USER_CREATE { get; set; }
        public int? I_USER_MODIF { get; set; }
        public DateTime? D_CREATE_DATE { get; set; }
        public DateTime? D_MODIF_DATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Usuario_Aplicacion, UsuarioAplicacionDTO>()
                .ForMember(dto => dto.I_USER_ID, et => et.MapFrom(a => a.I_ID_USUARIO))
                .ForMember(dto => dto.D_START_DATE, et => et.MapFrom(a => a.D_FECHA_INICIO))
                .ForMember(dto => dto.D_END_DATE, et => et.MapFrom(a => a.D_FECHA_FIN))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO))
                .ForMember(dto => dto.I_USER_CREATE, et => et.MapFrom(a => a.I_USUARIO_CREACION))
                .ForMember(dto => dto.D_CREATE_DATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
                .ForMember(dto => dto.I_USER_MODIF, et => et.MapFrom(a => a.I_USUARIO_MODIFICA))
                .ForMember(dto => dto.D_MODIF_DATE, et => et.MapFrom(a => a.D_FECHA_MODIFICA));
        }

        //private DateTime GetDate(string date)
        //{
        //    return Convert.ToDateTime(date).Date;
        //}

        //public void Mapping(Profile profine)
        //{
        //    profine.CreateMap<GetReniecResult, MinsaDTO>()
        //        .ForMember(dto => dto.paternal_surname, et => et.MapFrom(a => a.@string[1]))
        //        .ForMember(dto => dto.maternal_surname, et => et.MapFrom(a => a.@string[2]))
        //        .ForMember(dto => dto.name, et => et.MapFrom(a => a.@string[3]))
        //        .ForMember(dto => dto.birthdate, et => et.MapFrom(a => GetDate(a.@string[18].ToString())))
        //        .ForMember(dto => dto.date_issue_dni, et => et.MapFrom(a => GetDate(a.@string[19].ToString())))
        //        .ForMember(dto => dto.dni, et => et.MapFrom(a => a.@string[21]))
        //        .ForMember(dto => dto.nationality, et => et.MapFrom(a => a.@string[11]))
        //        .ForMember(dto => dto.address, et => et.MapFrom(a => a.@string[16]))
        //        .ForMember(dto => dto.department, et => et.MapFrom(a => a.@string[12]))
        //        .ForMember(dto => dto.province, et => et.MapFrom(a => a.@string[13]))
        //        .ForMember(dto => dto.district, et => et.MapFrom(a => a.@string[14]));
        //}
        //private string GetDate(string dateValue)
        //{
        //    DateTime result;
        //    var eval = DateTime.TryParseExact(dateValue, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        //    return result.ToString("dd/MM/yyyy");
        //}
    }
}
