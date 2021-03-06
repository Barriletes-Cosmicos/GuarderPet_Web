using GuarderPet.API.Data;
using GuarderPet.API.Data.Entities;
using GuarderPet.API.Helpers;
using GuarderPet.API.Models;
using GuarderPet.Common.Enums;
using GuarderPet.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuarderPet.API.Controllers
{
    public class UsersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IMailHelper _mailHelper;

        public UsersController(DataContext context, IUserHelper userHelper, ICombosHelper combosHelper,
            IConverterHelper converterHelper, IBlobHelper blobHelper, IMailHelper mailHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _blobHelper = blobHelper;
            _mailHelper = mailHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users
                .Include(x => x.DocumentType)
                .Include(x => x.Pets)
                .Include(x => x.Place)
                .Where(x => x.UserType == UserType.User)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            UserViewModel model = new UserViewModel
            {
                DocumentTypes = _combosHelper.GetComboDocumentTypes(),
                Place = _combosHelper.GetComboPlaces()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _converterHelper.ToUserAsync(model, true);
                user.UserType = UserType.User;
                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, user.UserType.ToString());

                string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                string tokenLink = Url.Action("ConfirmEmail", "Account", new
                {
                    userid = user.Id,
                    token = myToken
                }, protocol: HttpContext.Request.Scheme);

                Response response = _mailHelper.SendMail(model.Email, "GuarderPet - Confirmación de cuenta", $"<h1>GuarderPet - Confirmación de cuenta</h1>" +
                    $"Para habilitar el usuario, " +
                    $"por favor hacer clic en el siguiente enlace: </br></br><a href = \"{tokenLink}\">Confirmar Email</a>");

                return RedirectToAction(nameof(Index));
            }

            model.DocumentTypes = _combosHelper.GetComboDocumentTypes();
            model.Place = _combosHelper.GetComboPlaces();
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(Guid.Parse(id));
            if (user == null)
            {
                return NotFound();
            }

            UserViewModel model = _converterHelper.ToUserViewModel(user);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _converterHelper.ToUserAsync(model, false);
                await _userHelper.UpdateUserAsync(user);
                return RedirectToAction(nameof(Index));
            }

            model.DocumentTypes = _combosHelper.GetComboDocumentTypes();
            model.Place = _combosHelper.GetComboPlaces();
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(Guid.Parse(id));
            if (user == null)
            {
                return NotFound();
            }

            //await _blobHelper.DeleteBlobAsync(user.im, "users");
            await _userHelper.DeleteUserAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User user = await _context.Users
                .Include(x => x.DocumentType)
                .Include(x => x.Pets)
                .ThenInclude(x => x.Breed)
                .Include(x => x.Pets)
                .ThenInclude(x => x.Histories)
                .Include(x => x.Pets)
                .ThenInclude(x => x.PetType)
                .Include(x => x.Pets)
                .ThenInclude(x => x.PetPhotos)
                .Include(x => x.Place)
                .ThenInclude(x => x.PlaceName)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> AddPet(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User user = await _context.Users
                .Include(x => x.Pets)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            PetViewModel model = new PetViewModel
            {
                Breeds = _combosHelper.GetComboBreeds(),
                UserId = user.Id,
                PetTypes = _combosHelper.GetComboPetTypes()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPet(PetViewModel petViewModel)
        {
            User user = await _context.Users
                .Include(x => x.Pets)
                .FirstOrDefaultAsync(x => x.Id == petViewModel.UserId);
            if (user == null)
            {
                return NotFound();
            }

            Guid imageId = Guid.Empty;
            if (petViewModel.ImageFile != null)
            {
                imageId = await _blobHelper.UploadBlobAsync(petViewModel.ImageFile, "pets");
            }

            Pet pet = await _converterHelper.ToPetAsync(petViewModel, true);
            if (pet.PetPhotos == null)
            {
                pet.PetPhotos = new List<PetPhoto>();
            }

            pet.PetPhotos.Add(new PetPhoto
            {
                ImageId = imageId
            });

            try
            {
                user.Pets.Add(pet);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = user.Id });
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    ModelState.AddModelError(string.Empty, "Ya existe un vehículo con esa placa.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            petViewModel.Breeds = _combosHelper.GetComboBreeds();
            petViewModel.PetTypes = _combosHelper.GetComboPetTypes();
            return View(petViewModel);
        }

        public async Task<IActionResult> EditPet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pet pet = await _context.Pets
                .Include(x => x.User)
                .Include(x => x.Breed)
                .Include(x => x.PetType)
                .Include(x => x.PetPhotos)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            PetViewModel model = _converterHelper.ToPetViewModel(pet);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPet(int id, PetViewModel petViewModel)
        {
            if (id != petViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Pet pet = await _converterHelper.ToPetAsync(petViewModel, false);
                    _context.Pets.Update(pet);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { id = petViewModel.UserId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un vehículo con esta placa.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            petViewModel.Breeds = _combosHelper.GetComboBreeds();
            petViewModel.PetTypes = _combosHelper.GetComboPetTypes();
            return View(petViewModel);
        }

        public async Task<IActionResult> DeletePet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pet pet = await _context.Pets
                .Include(x => x.User)
                .Include(x => x.Breed)
                .Include(x => x.PetType)
                .Include(x => x.PetPhotos)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = pet.User.Id });
        }

        public async Task<IActionResult> DeleteImagePet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PetPhoto petPhoto = await _context.PetPhotos
                .Include(x => x.Pet)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (petPhoto == null)
            {
                return NotFound();
            }

            try
            {
                await _blobHelper.DeleteBlobAsync(petPhoto.ImageId, "pets");
            }
            catch { }

            _context.PetPhotos.Remove(petPhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(EditPet), new { id = petPhoto.Pet.Id });
        }

        public async Task<IActionResult> AddPetImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pet pet = await _context.Pets
                .FirstOrDefaultAsync(x => x.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            PetPhotoViewModel model = new()
            {
                PetId = pet.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPetImage(PetPhotoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "pets");
                Pet pet = await _context.Pets
                    .Include(x => x.PetPhotos)
                    .FirstOrDefaultAsync(x => x.Id == model.PetId);
                if (pet.PetPhotos == null)
                {
                    pet.PetPhotos = new List<PetPhoto>();
                }

                pet.PetPhotos.Add(new PetPhoto
                {
                    ImageId = imageId
                });

                _context.Pets.Update(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(EditPet), new { id = pet.Id });
            }

            return View(model);

        }

        public async Task<IActionResult> DetailsPet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pet pet = await _context.Pets
                .Include(x => x.User)
                .Include(x => x.Breed)
                .Include(x => x.PetPhotos)
                .Include(x => x.Histories)
                .ThenInclude(x => x.CareDescriptions)
                .ThenInclude(x => x.PetServices)
                .Include(x => x.Histories)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        public async Task<IActionResult> AddHistory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pet pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            HistoryViewModel model = new HistoryViewModel
            {
                PetId = pet.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddHistory(HistoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Pet pet = await _context.Pets
                    .Include(x => x.Histories)
                    .FirstOrDefaultAsync(x => x.Id == model.PetId);
                if (pet == null)
                {
                    return NotFound();
                }

                User user = await _userHelper.GetUserAsync(User.Identity.Name);
                PetServiceHistory serviceHistory = new PetServiceHistory
                {
                    RegisterDate = DateTime.UtcNow,
                    Comments = model.Comments,
                    User = user
                };

                if (pet.Histories == null)
                {
                    pet.Histories = new List<PetServiceHistory>();
                }

                pet.Histories.Add(serviceHistory);
                _context.Pets.Update(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DetailsPet), new { id = pet.Id });
            }

            return View(model);
        }

        public async Task<IActionResult> EditHistory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PetServiceHistory serviceHistory = await _context.PetServiceHistories
                .Include(x => x.Pet)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (serviceHistory == null)
            {
                return NotFound();
            }

            HistoryViewModel model = new HistoryViewModel
            {
                RegisterDate = serviceHistory.RegisterDate,
                Comments = serviceHistory.Comments,
                PetId = serviceHistory.Pet.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHistory(int id, HistoryViewModel historyViewModel)
        {
            if (ModelState.IsValid)
            {
                PetServiceHistory serviceHistory = await _context.PetServiceHistories.FindAsync(id);
                serviceHistory.RegisterDate = historyViewModel.RegisterDate;
                serviceHistory.Comments = historyViewModel.Comments;
                _context.PetServiceHistories.Update(serviceHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DetailsPet), new { id = historyViewModel.PetId });
            }

            return View(historyViewModel);
        }

        public async Task<IActionResult> DeleteHistory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PetServiceHistory serviceHistory = await _context.PetServiceHistories
                .Include(x => x.CareDescriptions)
                .Include(x => x.Pet)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (serviceHistory == null)
            {
                return NotFound();
            }

            _context.PetServiceHistories.Remove(serviceHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DetailsPet), new { id = serviceHistory.Pet.Id });
        }

        public async Task<IActionResult> DetailsHistory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PetServiceHistory serviceHistory = await _context.PetServiceHistories
                .Include(x => x.CareDescriptions)
                .ThenInclude(x => x.PetServices)
                .Include(x => x.Pet)
                .ThenInclude(x => x.PetPhotos)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (serviceHistory == null)
            {
                return NotFound();
            }

            return View(serviceHistory);
        }

        public async Task<IActionResult> AddDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PetServiceHistory serviceHistory = await _context.PetServiceHistories.FindAsync(id);
            if (serviceHistory == null)
            {
                return NotFound();
            }

            DetailViewModel model = new DetailViewModel
            {
                HistoryId = serviceHistory.Id,
                PetServices = _combosHelper.GetComboPetServices()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDetail(DetailViewModel detailViewModel)
        {
            if (ModelState.IsValid)
            {
                PetServiceHistory serviceHistory = await _context.PetServiceHistories
                    .Include(x => x.CareDescriptions)
                    .FirstOrDefaultAsync(x => x.Id == detailViewModel.HistoryId);
                if (serviceHistory == null)
                {
                    return NotFound();
                }

                if (serviceHistory.CareDescriptions == null)
                {
                    serviceHistory.CareDescriptions = new List<CareDescription>();
                }

                CareDescription detail = await _converterHelper.ToDetailAsync(detailViewModel, true);
                serviceHistory.CareDescriptions.Add(detail);
                _context.PetServiceHistories.Update(serviceHistory);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(DetailsHistory), new { id = detailViewModel.HistoryId });
            }

            detailViewModel.PetServices = _combosHelper.GetComboPetServices();
            return View(detailViewModel);
        }

        public async Task<IActionResult> EditDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CareDescription careDescription = await _context.CareDescriptions
                .Include(x => x.History)
                .Include(x => x.PetServices)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (careDescription == null)
            {
                return NotFound();
            }

            DetailViewModel model = _converterHelper.ToDetailViewModel(careDescription);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDetail(int id, DetailViewModel detailViewModel)
        {
            if (ModelState.IsValid)
            {
                CareDescription careDescription = await _converterHelper.ToDetailAsync(detailViewModel, false);
                _context.CareDescriptions.Update(careDescription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DetailsHistory), new { id = detailViewModel.HistoryId });
            }

            detailViewModel.PetServices = _combosHelper.GetComboPetServices();
            return View(detailViewModel);
        }

        public async Task<IActionResult> DeleteDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CareDescription careDescription = await _context.CareDescriptions
                .Include(x => x.History)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (careDescription == null)
            {
                return NotFound();
            }

            _context.CareDescriptions.Remove(careDescription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DetailsHistory), new { id = careDescription.History.Id });
        }
    }
}
