using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuarderPet.API.Data.Entities
{
    public class Pet
    {
        public int Id { get; set; }

        [Display(Name = "Edad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int PetAge { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string PetName { get; set; }

        [Display(Name = "Usuario Propietario")]
        public User User { get; set; }

        [Display(Name = "Raza")]
        public Breed Breed { get; set; }

        //    [Display(Name = "Fotos")]
        //    public ICollection<PhotoPet> Photos { get; set; }

        [Display(Name = "Historial de Servicios")]
        public ICollection<PetServiceHistory> Histories { get; set; }
    }
}
