using Projekt.Models.Common;
using Projekt.Service.Collate;
using System.Collections.Generic;
using X.PagedList;

namespace Projekt.Service.Common
{
    public interface IVehicleService
    {
        #region Make

        IVehicleMake GetMakeById(int id);

        void SaveChanges(IVehicleMake vehicleMake);

        void DeleteMake(IVehicleMake vehicleMake);

        
        IEnumerable<IVehicleMake> GetMake();

        IPagedList<IVehicleMake> FindMake(IFilter filter, ISorter sorter, IPager pager);

        #endregion Make

        #region Model

        IVehicleModel GetModelById(int id);

        void SaveChanges(IVehicleModel vehicleModel);

        void DeleteModel(IVehicleModel vehicleModel);

        IPagedList<IVehicleModel> FindModel(IFilter filter, ISorter sorter, IPager pager);

        #endregion Model
    }
}
