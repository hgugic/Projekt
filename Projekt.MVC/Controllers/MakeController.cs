using Microsoft.AspNetCore.Mvc;
using Projekt.Models;
using Projekt.Service.Common;


namespace Projekt.MVC.Controllers
{
    public class MakeController : Controller
    {
        private readonly IVehicleService vehicleService;


        public MakeController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        public IActionResult Administration() => View((vehicleService.GetMake()));

        public IActionResult Delete(int makeId=0)
        {
            if (makeId == 0)
            {
                return BadRequest();
            }
            else if (vehicleService.GetMakeById(makeId) == null)
            {
                return View(NotFound());
            }
            else
            {
                vehicleService.DeleteMake(makeId);
                return RedirectToAction("Administration", "Make", new { page = 1 });
            }
        }
        public ViewResult Edit(int makeId)
        {
            return View(vehicleService.GetMakeById(makeId));
        }


        [HttpPost]
        public IActionResult Edit(VehicleMake vehicleMakeEdit)
        {
            if (ModelState.IsValid)
            {
                vehicleService.SaveChanges(vehicleMakeEdit);
                return RedirectToAction("Administration", "Make", new { page = 1 });
            }
            return View(vehicleMakeEdit);
        }

        public IActionResult Create() => View("Edit", new VehicleMake());


    }
}
