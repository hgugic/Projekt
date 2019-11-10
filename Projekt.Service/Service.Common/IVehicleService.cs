using Projekt.Models.Common;
using Projekt.Service.Collate;
using System.Collections.Generic;

namespace Projekt.Service.Common
{
    public interface IVehicleService
    {
        #region Make

        IVehicleMake GetMakeById(int id);

        void SaveChanges(IVehicleMake vehicleMake);

        void DeleteMake(int id);

        IEnumerable<IVehicleMake> GetMake();

        #endregion Make

        #region Model

        IVehicleModel GetModelById(int id);

        void SaveChanges(IVehicleModel vehicleModel);

        void DeleteModel(int id);

        IEnumerable<IVehicleModel> FindModel(IFilter filter, ISorter sorter, IPager pager);

        #endregion Model
    }
}
