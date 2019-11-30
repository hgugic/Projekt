using Projekt.Models.Common;
using Projekt.Service.Collate;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace Projekt.Service.Common
{
    public interface IVehicleModelService
    {
        #region Model

        IVehicleModel GetModelById(int id);

        void SaveChanges(IVehicleModel vehicleModel);

        void DeleteModel(int id);

        IPagedList<IVehicleModel> FindModel(IFilter filter, ISorter sorter, IPager pager);

        #endregion Model
    }
}
