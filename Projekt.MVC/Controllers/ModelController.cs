using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projekt.Models;
using Projekt.MVC.Collate;
using Projekt.Service.Common;


namespace Projekt.MVC.Controllers
{
    public class ModelController : Controller
    {
        private readonly IVehicleService vehicleService;

        public ModelController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        public IActionResult Administration(int? makeId, string find, string sortDirection, int page = 1)
        {
            var models = vehicleService.FindModel(new Filter(find, makeId), new Sorter(sortDirection), new Pager(page, 3));

            ViewBag.Make = makeId;
            ViewBag.SortDirection = sortDirection;
            ViewBag.Find = find;
            
            ViewBag.Makers = new SelectList(vehicleService.GetMake(), "Id", "Name", makeId);

            return View(models);
        }

        public IActionResult Delete(int Id = 0)
        {
            if (Id == 0)
            {
                return BadRequest();
            }
            else if (vehicleService.GetModelById(Id) == null)
            {
                return View(NotFound());
            }
            else
            {
                vehicleService.DeleteModel(Id);
                return RedirectToAction("Administration", "Model", new { page = 1 });
            }
        }

        [HttpGet]
        public ViewResult Edit(int Id)
        {
            ViewBag.Makers = new SelectList(vehicleService.GetMake(), "Id", "Name", Id);
            return View(vehicleService.GetModelById(Id));
        }


        [HttpPost]
        public IActionResult Edit(VehicleModel vehicleModelEdit)
        {
            if (ModelState.IsValid)
            {
                vehicleService.SaveChanges(vehicleModelEdit);
                return RedirectToAction("Administration", "Make", new { page = 1 });
            }
            ViewBag.Makers = new SelectList(vehicleService.GetMake(), "Id", "Name", vehicleModelEdit.MakeId);
            return View(vehicleModelEdit);
        }

        public IActionResult Create()
        {
            ViewBag.Makers = new SelectList(vehicleService.GetMake(), "Id", "Name");
            return View("Edit", new VehicleModel());
        }
    }
}
