﻿using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.Operaciones;

namespace HRA.Application.UseCases.Ubigeo_.Queries.ListaTotalDistrito
{
    public record class DistritoDTO : IMapFrom<Ubigeo>
    {
        public int I_UBIGEO_ID { get; set; }
        public string V_DEPARTAMENT_CODE { get; set; }
        public string? V_PROVINCE_CODE { get; set; }
        public string? V_DISTRICT_CODE { get; set; }
        public string? V_DISTRICT { get; set; }
        public string? V_CAPITAL { get; set; }
        public string? V_ALTITUDE { get; set; }
        public string? V_LATITUDE { get; set; }
        public string? V_LONGITUDE { get; set; }
        public string B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Ubigeo, DistritoDTO>()
                .ForMember(dto => dto.I_UBIGEO_ID, et => et.MapFrom(a => a.I_ID_UBIGEO))
                .ForMember(dto => dto.V_DEPARTAMENT_CODE, et => et.MapFrom(a => a.V_CODIGO_DEPARTAMENTO))
                .ForMember(dto => dto.V_PROVINCE_CODE, et => et.MapFrom(a => a.V_CODIGO_PROVINCIA))
                .ForMember(dto => dto.V_DISTRICT_CODE, et => et.MapFrom(a => a.V_CODIGO_DISTRITO))
                .ForMember(dto => dto.V_DISTRICT, et => et.MapFrom(a => a.V_DISTRITO))
                .ForMember(dto => dto.V_CAPITAL, et => et.MapFrom(a => a.V_CAPITAL))
                .ForMember(dto => dto.V_ALTITUDE, et => et.MapFrom(a => a.V_ALTITUDE))
                .ForMember(dto => dto.V_LATITUDE, et => et.MapFrom(a => a.V_LATITUDE))
                .ForMember(dto => dto.V_LONGITUDE, et => et.MapFrom(a => a.V_LONGITUDE))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
