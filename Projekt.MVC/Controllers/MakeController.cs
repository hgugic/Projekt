using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Projekt.Models.Common;
using Projekt.MVC.Collate;
using Projekt.MVC.Mappings;
using Projekt.MVC.ViewModels;
using Projekt.Service.Common;


namespace Projekt.MVC.Controllers
{
    public class MakeController : Controller
    {
        private readonly IVehicleService vehicleService;
        private readonly IMapper mapper;

        public MakeController(IVehicleService vehicleService, IMapper mapper)
        {
            this.vehicleService = vehicleService;
            this.mapper = mapper;
        }

        public IActionResult Administration(string find, string sortDirection, int page = 1)
        {
            var makers = vehicleService.FindMake(new Filter(find), new Sorter(sortDirection), new Pager(page, 3))
                                       .ToMappedPagedList<IVehicleMake, VehicleMakeViewModel>(mapper);

            ViewBag.Find = find;
            ViewBag.SortDirection = sortDirection;

            return View(makers);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                vehicleService.DeleteMake(vehicleService.GetMakeById(id));
                return RedirectToAction("Administration", "Make", new { page = 1 });
            }
            catch (System.ArgumentException)
            {
                return View(NotFound());
            }

        }
        public ViewResult Edit(int makeId)
        {
            return View(mapper.Map<VehicleMakeViewModel>(vehicleService.GetMakeById(makeId)));
        }


        [HttpPost]
        public IActionResult Edit(VehicleMakeViewModel vehicleMakeEdit)
        {
            if (ModelState.IsValid)
            {
                vehicleService.SaveChanges(vehicleMakeEdit);
                return RedirectToAction("Administration", "Make", new { page = 1 });
            }
            return View(vehicleMakeEdit);
        }

        public IActionResult Create() => View("Edit", new VehicleMakeViewModel());


    }
}
