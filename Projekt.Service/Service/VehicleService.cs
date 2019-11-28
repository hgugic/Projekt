﻿using AutoMapper;
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
using X.PagedList;

namespace Projekt.Service
{
    public class VehicleService : IVehicleService
    {

        private readonly VehicleDbContext context;
        private readonly IMapper mapper;

        public VehicleService(VehicleDbContext context)
        {
            this.context = context;
            AutoMapperMap.ConfigureMapping();
            mapper = AutoMapperMap.GetMapper();
        }

        public void DeleteMake(IVehicleMake vehicleMake)
        {
            VehicleMake make = context.VehicleMakers.FirstOrDefault(m => m == mapper.Map<VehicleMake>(vehicleMake));
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

        public void DeleteModel(IVehicleModel vehicleModel)
        {
            VehicleModel model = context.VehicleModels.FirstOrDefault(m => m == mapper.Map<VehicleModel>(vehicleModel));
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

        public IEnumerable<IVehicleMake> GetMake()
        {
            return mapper.Map<IEnumerable<IVehicleMake>>(context.VehicleMakers.OrderBy(v => v.Name)
                                                                              .Select(x=> new VehicleMake() { Id = x.Id, Name = x.Name}));
        }

        public IPagedList<IVehicleMake> FindMake(IFilter filter, ISorter sorter, IPager pager)
        {
            var makers = context.VehicleMakers.Where(m => string.IsNullOrEmpty(filter.Search) ? m != null : m.Name.Contains(filter.Search))
                                              .OrderByDescending(x => sorter.SortDirection == "dsc" ? x.Name : "")
                                              .OrderBy(x => string.IsNullOrEmpty(sorter.SortDirection) || sorter.SortDirection == "asc" ? x.Name : "");

            return mapper.Map<IEnumerable<IVehicleMake>>(makers).ToPagedList(pager.CurrentPage, pager.PageSize);

        }

        public IPagedList<IVehicleModel> FindModel(IFilter filter, ISorter sorter, IPager pager)
        {
            var models = context.VehicleModels.Where(m => string.IsNullOrEmpty(filter.Search) ? m!=null : m.Name.Contains(filter.Search))
                                              .Where(m => filter.MakeId == null ?  m != null : m.MakeId == filter.MakeId).Include("Make")
                                              .OrderByDescending(x => sorter.SortDirection == "dsc" ? x.Name : "")
                                              .OrderBy(x => string.IsNullOrEmpty(sorter.SortDirection) || sorter.SortDirection == "asc" ? x.Name : "");

            return mapper.Map<IEnumerable<IVehicleModel>>(models).ToPagedList(pager.CurrentPage, pager.PageSize);

        }

        public IVehicleMake GetMakeById(int id)
        {
            return mapper.Map<IVehicleMake>(context.VehicleMakers.FirstOrDefault(x => x.Id == id));
        }

        public IVehicleModel GetModelById(int id)
        {
            return mapper.Map<IVehicleModel>(context.VehicleModels.FirstOrDefault(x => x.Id == id));
        }

        public void SaveChanges(IVehicleMake vehicleMake)
        {
            if (vehicleMake.Id == 0)
            {
                context.Add(mapper.Map<VehicleMake>(vehicleMake));
                context.SaveChanges();
            }
            else
            {
                var vehicle = context.VehicleMakers.Attach(mapper.Map<VehicleMake>(vehicleMake));
                vehicle.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void SaveChanges(IVehicleModel vehicleModel)
        {
            if (vehicleModel.Id == 0)
            {
                context.Add(mapper.Map<VehicleModel>(vehicleModel));
                context.SaveChanges();
            }
            else
            {
                var vehicle = context.VehicleModels.Attach(mapper.Map<VehicleModel>(vehicleModel));
                vehicle.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

    }
}
