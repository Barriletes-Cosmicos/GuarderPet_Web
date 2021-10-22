using GuarderPet.API.Data.Entities;
using GuarderPet.API.Models;
using System.Threading.Tasks;

namespace GuarderPet.API.Helpers
{
    public interface IConverterHelper
    {
        Task<User> ToUserAsync(UserViewModel model, bool isNew);
        UserViewModel ToUserViewModel(User user);
        Task<Pet> ToPetAsync(PetViewModel model, bool isNew);
        PetViewModel ToPetViewModel(Pet pet);
        Task<CareDescription> ToDetailAsync(DetailViewModel model, bool isNew);
        DetailViewModel ToDetailViewModel(CareDescription detail);
    }
}
