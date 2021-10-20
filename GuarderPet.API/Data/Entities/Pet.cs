using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
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
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Breed Breed { get; set; }

        [Display(Name = "Tipo de mascota")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public PetType PetType { get; set; }

        [Display(Name = "Fotos")]
        public ICollection<PetPhoto> PetPhotos { get; set; }

        [Display(Name = "# Fotos")]
        public int PetsPhotosCount => PetPhotos == null ? 0 : PetPhotos.Count;

        public string ImageFullPath => PetPhotos == null || PetPhotos.Count == 0
            ? $"https://localhost:44396/images/noimage.png"
            : PetPhotos.FirstOrDefault().ImageFullPath;

        [Display(Name = "Historial de Servicios")]
        public ICollection<PetServiceHistory> Histories { get; set; }

        [Display(Name = "# Historial")]
        public int HistoriesCount => Histories == null ? 0 : Histories.Count;
    }
}
