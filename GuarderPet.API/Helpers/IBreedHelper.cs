using GuarderPet.API.Data.Entities;
using GuarderPet.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuarderPet.API.Helpers
{
    public interface IBreedHelper
    {
        Task<Breed> AddBreedAsync(BreedViewModel model);
    }
}
