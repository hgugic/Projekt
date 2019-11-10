using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt.Service.DAL.Entities
{
    public class VehicleMake : Vehicle
    {
        public ICollection<VehicleModel> VehicleModels { get; set; }

    }
}
