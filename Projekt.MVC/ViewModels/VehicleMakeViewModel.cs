using Projekt.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Projekt.MVC.ViewModels
{
    public class VehicleMakeViewModel : IVehicleMake
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "Naziv")]
        [Required(ErrorMessage = "Naziv je obavezno polje")]
        [MaxLength(30, ErrorMessage = "Predugačak naziv")]
        public string Name { get; set; }
        [Display(Name = "Skraćenica")]
        [Required(ErrorMessage = "Skraćenica je obavezno polje")]
        [MaxLength(10, ErrorMessage = "Predugačka skraćenica")]
        public string Abrv { get; set; }
    }
}
