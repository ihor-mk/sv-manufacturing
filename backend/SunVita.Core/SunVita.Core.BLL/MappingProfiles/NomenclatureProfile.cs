using AutoMapper;
using SunVita.Core.Common.DTO.Nomenclature;
using SunVita.Core.DAL.Entities;

namespace SunVita.Core.BLL.MappingProfiles
{
    public class NomenclatureProfile : Profile
    {
        public NomenclatureProfile() 
        {
            CreateMap<Nomenclature, NewNomenclatureDto>();
            CreateMap<NewNomenclatureDto, Nomenclature>();
        }
    }
}
