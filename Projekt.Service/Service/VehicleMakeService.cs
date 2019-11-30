using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Projekt.Models.Common;
using Projekt.Service.Collate;
using Projekt.Service.DAL;
using Projekt.Service.DAL.Entities;
using Projekt.Service.Common;
using Projekt.Service.Service.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;

namespace Projekt.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly VehicleDbContext context;
        private readonly IMapper mapper;

        public VehicleMakeService(VehicleDbContext context)
        {
            this.context = context;
            AutoMapperMap.ConfigureMapping();
            mapper = AutoMapperMap.GetMapper();
        }

        public void DeleteMake(int id)
        {
            VehicleMake make = context.VehicleMakers.Find(id);
            if (make != null)
            {
                context.Remove(make);
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Vehicle make doesn't exist");
            }
        }

        public IEnumerable<IVehicleMake> GetMake()
        {
            return mapper.Map<IEnumerable<IVehicleMake>>(context.VehicleMakers.OrderBy(v => v.Name)
                                                                              .Select(x => new VehicleMake() { Id = x.Id, Name = x.Name }));
        }

        public IPagedList<IVehicleMake> FindMake(IFilter filter, ISorter sorter, IPager pager)
        {
            var makers = context.VehicleMakers.Where(m => string.IsNullOrEmpty(filter.Search) ? m != null : m.Name.Contains(filter.Search))
                                              .OrderByDescending(x => sorter.SortDirection == "dsc" ? x.Name : "")
                                              .OrderBy(x => string.IsNullOrEmpty(sorter.SortDirection) || sorter.SortDirection == "asc" ? x.Name : "");

            return mapper.Map<IEnumerable<IVehicleMake>>(makers).ToPagedList(pager.CurrentPage, pager.PageSize);

        }

        public IVehicleMake GetMakeById(int id)
        {
            return mapper.Map<IVehicleMake>(context.VehicleMakers.FirstOrDefault(x => x.Id == id));
        }

        public void SaveChanges(IVehicleMake vehicleMake)
        {
            var editedMaker = mapper.Map<VehicleMake>(vehicleMake);
            context.Entry(editedMaker).State = context.VehicleMakers.Any(m => m.Id == editedMaker.Id) ? EntityState.Modified : EntityState.Added;
            context.SaveChanges();
        }
    }
}
