using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Projekt.Models.Common;
using Projekt.Service.Collate;
using Projekt.Service.Common;
using Projekt.Service.DAL;
using Projekt.Service.DAL.Entities;
using Projekt.Service.Service.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;

namespace Projekt.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly VehicleDbContext context;
        private readonly IMapper mapper;
        public VehicleModelService(VehicleDbContext context)
        {
            this.context = context;
            AutoMapperMap.ConfigureMapping();
            mapper = AutoMapperMap.GetMapper();
        }

        public void DeleteModel(int id)
        {
            VehicleModel model = context.VehicleModels.Find(id);
            if (model != null)
            {
                context.Remove(model);
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Vehicle model doesn't exist");
            }

        }



        public IPagedList<IVehicleModel> FindModel(IFilter filter, ISorter sorter, IPager pager)
        {
            var models = context.VehicleModels.Where(m => string.IsNullOrEmpty(filter.Search) ? m != null : m.Name.Contains(filter.Search))
                                              .Where(m => filter.MakeId == null ? m != null : m.MakeId == filter.MakeId).Include("Make")
                                              .OrderByDescending(x => sorter.SortDirection == "dsc" ? x.Name : "")
                                              .OrderBy(x => string.IsNullOrEmpty(sorter.SortDirection) || sorter.SortDirection == "asc" ? x.Name : "");

            return mapper.Map<IEnumerable<IVehicleModel>>(models).ToPagedList(pager.CurrentPage, pager.PageSize);

        }



        public IVehicleModel GetModelById(int id)
        {
            return mapper.Map<IVehicleModel>(context.VehicleModels.FirstOrDefault(x => x.Id == id));
        }



        public void SaveChanges(IVehicleModel vehicleModel)
        {
            var editedModel = mapper.Map<VehicleModel>(vehicleModel);
            context.Entry(editedModel).State = context.VehicleModels.Any(m => m.Id == editedModel.Id) ?  EntityState.Modified : EntityState.Added;
            context.SaveChanges();
        }
    }
}
