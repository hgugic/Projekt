using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Common
{
    public interface IVehicleMake
    {
        int Id { get; set; }

        [Display(Name = "Naziv")]
        [Required(ErrorMessage = "Naziv je obavezno polje")]
        [MaxLength(30, ErrorMessage = "Predugačak naziv")]
        string Name { get; set; }

        [Display(Name = "Skraćenica")]
        [Required(ErrorMessage = "Skraćenica je obavezno polje")]
        [MaxLength(10, ErrorMessage = "Predugačka skraćenica")]
        string Abrv { get; set; }
    }
}
