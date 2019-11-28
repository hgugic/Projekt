using AutoMapper;
using Projekt.Models.Common;
using Projekt.MVC.Models;
using Projekt.MVC.Models.Common;
using Projekt.MVC.ViewModels;

namespace Projekt.MVC.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VehicleMakeViewModel, IVehicleMake>();
            CreateMap<IVehicleMake, VehicleMakeViewModel>();

            CreateMap<IVehicleMakePrimary, IVehicleMake>();
            CreateMap<IVehicleMake, IVehicleMakePrimary>();

            CreateMap<VehicleMakePrimary, IVehicleMake>();
            CreateMap<IVehicleMake, VehicleMakePrimary>();

            CreateMap<VehicleModelViewModel, IVehicleModel>();
            CreateMap<IVehicleModel, VehicleModelViewModel>();
        }
    }
}
