using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuarderPet.API.Models
{
    public class BreedViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Raza")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string BreedTittle { get; set; }

        [Display(Name = "Tipo")]
        public IEnumerable<SelectListItem> PetType { get; set; }
    }
}
