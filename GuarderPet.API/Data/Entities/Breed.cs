using System.ComponentModel.DataAnnotations;

namespace GuarderPet.API.Data.Entities
{
    public class Breed
    {
        public int Id { get; set; }

        [Display(Name = "Raza")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string BreedTittle { get; set; }

        [Display(Name = "Tipo")]
        public PetType PetType { get; set; }
    }
}
