using AutoMapper;
using Projekt.Models.Common;
using Projekt.Service.DAL.Entities;


namespace Projekt.Service.Service.Mappings
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VehicleMake, IVehicleMake>();
            CreateMap<IVehicleMake, VehicleMake>();

            CreateMap<VehicleModel, IVehicleModel>();
            CreateMap<IVehicleModel, VehicleModel>();
        }
    }
}
