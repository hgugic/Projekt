using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projekt.Models.Common;
using Projekt.MVC.Collate;
using Projekt.MVC.Mappings;
using Projekt.MVC.Models.Common;
using Projekt.MVC.ViewModels;
using Projekt.Service.Common;
using System.Collections.Generic;

namespace Projekt.MVC.Controllers
{
    public class ModelController : Controller
    {
        private readonly IVehicleService vehicleService;
        private readonly IMapper mapper;

        public ModelController(IVehicleService vehicleService, IMapper mapper)
        {
            this.vehicleService = vehicleService;
            this.mapper = mapper;
        }

        public IActionResult Administration(int? makeId, string find, string sortDirection, int page = 1)
        {
            var models = vehicleService.FindModel(new Filter(find, makeId), new Sorter(sortDirection), new Pager(page, 3))
                                       .ToMappedPagedList<IVehicleModel, VehicleModelViewModel>(mapper);

            ViewBag.Make = makeId;
            ViewBag.SortDirection = sortDirection;
            ViewBag.Find = find;
            
            ViewBag.Makers = new SelectList(mapper.Map<IEnumerable<IVehicleMakePrimary>>(vehicleService.GetMake()), "Id", "Name", makeId);

            return View(models);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                vehicleService.DeleteModel(vehicleService.GetModelById(id));
                return RedirectToAction("Administration", "Model", new { page = 1 });
            }
            catch (System.ArgumentException)
            {
                return View(NotFound());
            }

        }

        [HttpGet]
        public ViewResult Edit(int Id)
        {
            ViewBag.Makers = new SelectList(mapper.Map<IEnumerable<IVehicleMakePrimary>>(vehicleService.GetMake()), "Id", "Name", Id);
            return View(mapper.Map<VehicleModelViewModel>(vehicleService.GetModelById(Id)));
        }


        [HttpPost]
        public IActionResult Edit(VehicleModelViewModel vehicleModelEdit)
        {
            if (ModelState.IsValid)
            {
                vehicleService.SaveChanges(vehicleModelEdit);
                return RedirectToAction("Administration", "Make", new { page = 1 });
            }
            ViewBag.Makers = new SelectList(mapper.Map<IEnumerable<IVehicleMakePrimary>>(vehicleService.GetMake()), "Id", "Name", vehicleModelEdit.MakeId);
            return View(vehicleModelEdit);
        }

        public IActionResult Create()
        {
            ViewBag.Makers = new SelectList(mapper.Map<IEnumerable<IVehicleMakePrimary>>(vehicleService.GetMake()), "Id", "Name");
            return View("Edit", new VehicleModelViewModel());
        }
    }
}
