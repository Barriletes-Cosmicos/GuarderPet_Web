using GuarderPet.API.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuarderPet.API.Models
{
    public class PetViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Edad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int PetAge { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string PetName { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Raza")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int BreedId { get; set; }
        public IEnumerable<SelectListItem> Breeds { get; set; }

        [Display(Name = "Tipo de mascota")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int PetTypeId { get; set; }
        public IEnumerable<SelectListItem> PetTypes { get; set; }

        [Display(Name = "Foto")]
        public IFormFile ImageFile { get; set; }

        public ICollection<PetPhoto> PetPhotos { get; set; }
    }
}
