using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuarderPet.API.Models
{
    public class HistoryViewModel
    {
        public int PetId { get; set; }

        [Display(Name = "Fecha de registro")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }
    }
}
