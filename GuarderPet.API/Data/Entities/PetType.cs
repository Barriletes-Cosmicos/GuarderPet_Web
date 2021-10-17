using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuarderPet.API.Data.Entities
{
    public class PetType
    {
        public int Id { get; set; }

        [Display(Name = "Tipo")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Type { get; set; }

        public ICollection<Breed> Breeds { get; set; }
    }
}
