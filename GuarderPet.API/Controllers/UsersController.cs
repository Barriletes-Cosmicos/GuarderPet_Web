using GuarderPet.API.Data;
using GuarderPet.API.Data.Entities;
using GuarderPet.API.Helpers;
using GuarderPet.API.Models;
using GuarderPet.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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

        public UsersController(DataContext context, IUserHelper userHelper, ICombosHelper combosHelper,
            IConverterHelper converterHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users
                .Include(x => x.DocumentType)
                //.Include(x => x.Pet)
                .Where(x => x.UserType == UserType.User)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            UserViewModel model = new UserViewModel
            {
                DocumentTypes = _combosHelper.GetComboDocumentTypes()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                //if (model.ImageFile != null)
                //{
                //    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                //}

                User user = await _converterHelper.ToUserAsync(model, true);
                user.UserType = UserType.User;
                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, user.UserType.ToString());

                //string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                //string tokenLink = Url.Action("ConfirmEmail", "Account", new
                //{
                //    userid = user.Id,
                //    token = myToken
                //}, protocol: HttpContext.Request.Scheme);

                //Response response = _mailHelper.SendMail(model.Email, "GuarderPet - Confirmación de cuenta", $"<h1>GuarderPet - Confirmación de cuenta</h1>" +
                //    $"Para habilitar el usuario, " +
                //    $"por favor hacer clic en el siguiente enlace: </br></br><a href = \"{tokenLink}\">Confirmar Email</a>");

                return RedirectToAction(nameof(Index));
            }

            model.DocumentTypes = _combosHelper.GetComboDocumentTypes();
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
                //Guid imageId = model.ImageId;
                //if (model.ImageFile != null)
                //{
                //    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                //}

                User user = await _converterHelper.ToUserAsync(model, false);
                await _userHelper.UpdateUserAsync(user);
                return RedirectToAction(nameof(Index));
            }

            model.DocumentTypes = _combosHelper.GetComboDocumentTypes();
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

            //await _blobHelper.DeleteBlobAsync(user.ImageId, "users");
            await _userHelper.DeleteUserAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            //    .Include(x => x.Vehicles)
            //    .ThenInclude(x => x.VehiclePhotos)
            User user = await _context.Users
                .Include(x => x.DocumentType)
                .Include(x => x.Pets)
                .ThenInclude(x => x.Breed)
                .Include(x => x.Pets)
                .ThenInclude(x => x.Histories)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
