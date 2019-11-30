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
        private readonly IVehicleModelService modelService;
        private readonly IVehicleMakeService makerService;
        private readonly IMapper mapper;

        public ModelController(IVehicleModelService modelService, IVehicleMakeService makerService, IMapper mapper)
        {
            this.modelService = modelService;
            this.makerService = makerService;
            this.mapper = mapper;
        }

        public IActionResult Administration(int? makeId, string find, string sortDirection, int page = 1)
        {
            var models = modelService.FindModel(new Filter(find, makeId), new Sorter(sortDirection), new Pager(page, 3))
                                       .ToMappedPagedList<IVehicleModel, VehicleModelViewModel>(mapper);

            ViewBag.Make = makeId;
            ViewBag.SortDirection = sortDirection;
            ViewBag.Find = find;
            
            ViewBag.Makers = new SelectList(mapper.Map<IEnumerable<IVehicleMakePrimary>>(makerService.GetMake()), "Id", "Name", makeId);

            return View(models);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                modelService.DeleteModel(id);
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
            ViewBag.Makers = new SelectList(mapper.Map<IEnumerable<IVehicleMakePrimary>>(makerService.GetMake()), "Id", "Name", Id);
            return View(mapper.Map<VehicleModelViewModel>(modelService.GetModelById(Id)));
        }


        [HttpPost]
        public IActionResult Edit(VehicleModelViewModel vehicleModelEdit)
        {
            if (ModelState.IsValid)
            {
                modelService.SaveChanges(mapper.Map < IVehicleModel > (vehicleModelEdit));
                return RedirectToAction("Administration", "Make", new { page = 1 });
            }
            ViewBag.Makers = new SelectList(mapper.Map<IEnumerable<IVehicleMakePrimary>>(makerService.GetMake()), "Id", "Name", vehicleModelEdit.MakeId);
            return View(vehicleModelEdit);
        }

        public IActionResult Create()
        {
            ViewBag.Makers = new SelectList(mapper.Map<IEnumerable<IVehicleMakePrimary>>(makerService.GetMake()), "Id", "Name");
            return View("Edit", new VehicleModelViewModel());
        }
    }
}
