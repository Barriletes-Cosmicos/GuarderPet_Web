using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuarderPet.API.Models
{
    public class DetailViewModel
    {
        public int Id { get; set; }

        public int HistoryId { get; set; }

        [Display(Name = "Servicio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un procedimiento.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ServiceId { get; set; }

        [Display(Name = "Listado de servicios")]
        public IEnumerable<SelectListItem> PetServices { get; set; }

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
