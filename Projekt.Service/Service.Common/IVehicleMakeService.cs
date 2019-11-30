using Projekt.Models.Common;
using Projekt.Service.Collate;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace Projekt.Service.Common
{
    public interface IVehicleMakeService
    {
        #region Make

        IVehicleMake GetMakeById(int id);

        void SaveChanges(IVehicleMake vehicleMake);

        void DeleteMake(int id);


        IEnumerable<IVehicleMake> GetMake();

        IPagedList<IVehicleMake> FindMake(IFilter filter, ISorter sorter, IPager pager);

        #endregion Make
    }
}
