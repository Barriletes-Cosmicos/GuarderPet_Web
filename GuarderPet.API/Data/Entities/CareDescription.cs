using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuarderPet.API.Data.Entities
{
    public class CareDescription
    {
        public int Id { get; set; }

        [Display(Name = "Listado de servicios")]
        public PetService PetServices { get; set; }

        [Display(Name = "Precio servicio")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal ServicePrice { get; set; }

        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal TotalPrice => TotalPrice + ServicePrice;

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

    }
}
