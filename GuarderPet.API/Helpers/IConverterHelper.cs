using GuarderPet.API.Data.Entities;
using GuarderPet.API.Models;
using System.Threading.Tasks;

namespace GuarderPet.API.Helpers
{
    public interface IConverterHelper
    {
        Task<User> ToUserAsync(UserViewModel model, bool isNew);
        UserViewModel ToUserViewModel(User user);
        //Task<Vehicle> ToVehicleAsync(VehicleViewModel model, bool isNew);
        //VehicleViewModel ToVehicleViewModel(Vehicle vehicle);
        //Task<Detail> ToDetailAsync(DetailViewModel model, bool isNew);
        //DetailViewModel ToDetailViewModel(Detail detail);
    }
}
