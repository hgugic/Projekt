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

            CreateMap<Models.VehicleMake, IVehicleMake>();
            CreateMap<IVehicleMake, Models.VehicleMake>();

            CreateMap<Models.VehicleMake, VehicleMake>();
            CreateMap<VehicleMake, Models.VehicleMake>();

            CreateMap<Models.VehicleModel, IVehicleModel>();
            CreateMap<IVehicleModel, Models.VehicleModel>();

            CreateMap<VehicleModel, IVehicleModel>();
            CreateMap<IVehicleModel, VehicleModel>();

            CreateMap<Models.VehicleModel, VehicleModel>();
            CreateMap<VehicleModel, Models.VehicleModel>();


        }
    }
}
