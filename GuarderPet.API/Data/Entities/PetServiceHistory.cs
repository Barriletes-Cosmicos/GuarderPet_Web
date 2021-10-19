using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GuarderPet.API.Data.Entities
{
    public class PetServiceHistory
    {
        public int Id { get; set; }

        [Display(Name = "Mascota")]
        public Pet Pet { get; set; }

        [JsonIgnore]
        [Display(Name = "Usuario")]
        public User User { get; set; }
        public ICollection<CareDescription> CareDescriptions { get; set; }

        [Display(Name = "# Detalles")]
        public int CareDescriptionsCount => CareDescriptions == null ? 0 : CareDescriptions.Count;

        [Display(Name = "Fecha de Inicio")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        public DateTime InitDate { get; set; }

        [Display(Name = "Fecha de Finalizacion")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        public DateTime InitDateLocal => InitDate.ToLocalTime();

        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        public DateTime EndDateLocal => EndDate.ToLocalTime();

        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Total { get; set; }
    }
}
