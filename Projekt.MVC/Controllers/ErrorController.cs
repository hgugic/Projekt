using Microsoft.AspNetCore.Mvc;

namespace Projekt.MVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpErrorCodeHandler(int statusCode)
        {
            switch (statusCode)
            {

                case 400:
                    ViewBag.ErrorMessage = "Nije moguće procesirati zahtjev";
                    break;
                case 404:
                    ViewBag.ErrorMessage = "Resurs koji tražite nije nađen";
                    break;

            }

            return View("NotFound");
        }

        [Route("Error")]
        public IActionResult Error() => View("Error");

    }
}
