using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GuarderPet.API.Data.Entities
{
    public class CareDescription
    {
        public int Id { get; set; }

        [JsonIgnore]
        [Display(Name = "Historia")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public PetServiceHistory History { get; set; }

        [Display(Name = "Listado de servicios")]
        public PetService PetServices { get; set; }

        [Display(Name = "Precio servicio")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal ServicePrice { get; set; }

        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

    }
}
