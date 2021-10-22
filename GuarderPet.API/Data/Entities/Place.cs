using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GuarderPet.API.Data.Entities
{
    public class Place
    {
        public int Id { get; set; }

        [Display(Name = "Usuario")]
        [JsonIgnore]
        public User User { get; set; }

        [Display(Name = "Direccion")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string direction { get; set; }

        //[Display(Name = "Fotos")]
        //public ICollection<PhotoPlace> Photos { get; set; }
    }
}
