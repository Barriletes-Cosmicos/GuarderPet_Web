using GuarderPet.API.Data;
using GuarderPet.API.Data.Entities;
using GuarderPet.API.Models;
using System.Threading.Tasks;

namespace GuarderPet.API.Helpers
{
    public class BreedHelper : IBreedHelper
    {
        private readonly DataContext _context;

        public BreedHelper(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<Breed> AddBreedAsync(BreedViewModel model)
        {
            Breed breed = new Breed
            {
                BreedTittle = model.BreedTittle,
                PetType = await _context.PetTypes.FindAsync(model.Id)
            };

            _context.Add(breed);
            await _context.SaveChangesAsync();

            return breed;
        }

        public async Task<Breed> DeleteBreedAsync(int id)
        {
            Breed breed = await _context.Breeds.FindAsync(id);
            _context.Remove(breed);

            return breed;
        }
    }
}
