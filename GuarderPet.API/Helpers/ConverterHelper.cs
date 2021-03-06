using GuarderPet.API.Data;
using GuarderPet.API.Data.Entities;
using GuarderPet.API.Models;
using System;
using System.Threading.Tasks;
using GuarderPet.API.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GuarderPet.API.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        public async Task<CareDescription> ToDetailAsync(DetailViewModel model, bool isNew)
        {
            return new CareDescription
            {
                Id = isNew ? 0 : model.Id,
                History = await _context.PetServiceHistories.FindAsync(model.HistoryId),
                ServicePrice = model.ServicePrice,
                Comments = model.Comments,
                PetServices = await _context.PetServices.FindAsync(model.ServiceId)
            };
        }

        public DetailViewModel ToDetailViewModel(CareDescription detail)
        {
            return new DetailViewModel
            {
                HistoryId = detail.History.Id,
                Id = detail.Id,
                PetServices = _combosHelper.GetComboPetServices(),
                ServicePrice = detail.ServicePrice,
                ServiceId = detail.PetServices.Id,
                Comments = detail.Comments
            };
        }

        public async Task<User> ToUserAsync(UserViewModel model, bool isNew)
        {
            return new User
            {
                Address = model.Address,
                Document = model.Document,
                DocumentType = await _context.DocumentTypes.FindAsync(model.DocumentTypeId),
                Email = model.Email,
                FirstName = model.FirstName,
                Id = isNew ? Guid.NewGuid().ToString() : model.Id,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email,
                UserType = model.UserType,
            };
        }

        public UserViewModel ToUserViewModel(User user)
        {
            return new UserViewModel
            {
                Address = user.Address,
                Document = user.Document,
                DocumentTypeId = user.DocumentType.Id,
                DocumentTypes = _combosHelper.GetComboDocumentTypes(),
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                UserType = user.UserType,
                Place = _combosHelper.GetComboPlaces()
            };
        }

        public async Task<Pet> ToPetAsync(PetViewModel model, bool isNew)
        {
            Breed breed = await _context.Breeds
                                               .Include(x=>x.PetType)
                                               .FirstOrDefaultAsync(x => x.Id == model.BreedId);

            PetType petType = breed.PetType;

            return new Pet
            {
                Breed = breed,
                PetName = model.PetName,
                PetAge = model.PetAge,
                PetType = petType,
                Id = isNew ? 0 : model.Id
            };
        }

        public PetViewModel ToPetViewModel(Pet pet)
        {
            return new PetViewModel
            {
                BreedId = pet.Breed.Id,
                Breeds = _combosHelper.GetComboBreeds(),
                PetName = pet.PetName,
                PetAge = pet.PetAge,
                PetTypeId = pet.PetType.Id,
                PetTypes = _combosHelper.GetComboPetTypes(),
                Id = pet.Id,
                UserId = pet.User.Id,
                PetPhotos = pet.PetPhotos
            };
        }
    }
}
