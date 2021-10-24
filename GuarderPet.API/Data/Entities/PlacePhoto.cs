using System;
using System.ComponentModel.DataAnnotations;

namespace GuarderPet.API.Data.Entities
{
    public class PlacePhoto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Place place { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:44396/images/noimage.png"
            : $"https://petsbc.blob.core.windows.net/pets/{ImageId}";

    }
}
