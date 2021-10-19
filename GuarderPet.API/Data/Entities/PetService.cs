using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuarderPet.API.Data.Entities
{
    public class PetService
    {
        public int Id { get; set; }

        [Display(Name = "Detalle")]
        [MaxLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string ServiceDetail { get; set; }

        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Price { get; set; }
        public ICollection<CareDescription> CareDescriptions { get; set; }
    }
}
