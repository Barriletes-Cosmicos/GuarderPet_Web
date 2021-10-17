using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuarderPet.API.Data.Entities
{
    public class PetServiceHistory
    {
        public int Id { get; set; }

        [Display(Name = "Mascota")]
        public Pet Pet { get; set; }

        [Display(Name = "Usuario")]
        public User User { get; set; }

        [Display(Name = "Procedimiento")]
        public CareDescription CareDescription { get; set; }

        [Display(Name = "Fecha de Inicio")]
        public DateTime InitDate { get; set; }

        [Display(Name = "Fecha de Finalizacion")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Total { get; set; }
    }
}
