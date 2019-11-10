using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt.Service.DAL.Entities
{
    public class VehicleModel : Vehicle
    {
        public int MakeId { get; set; }

        public VehicleMake Make { get; set; }
    }
}
