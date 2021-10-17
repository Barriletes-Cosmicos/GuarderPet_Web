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
        public ICollection<PetService> PetServices { get; set; }

    }
}
