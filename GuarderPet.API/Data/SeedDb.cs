using GuarderPet.API.Data.Entities;
using GuarderPet.API.Helpers;
using GuarderPet.Common.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace GuarderPet.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckDocumentTypesAsync();
            await CheckRolesAsycn();
            await CheckUserAsync("101010", "Santiago", "Osorio", "osorio@guarderpet.com", "300 123 4567", "Calle 1 # 2 - 3", UserType.Admin);
            await CheckUserAsync("101011", "Lucas", "Giraldo", "lukitax_solo_millos@guarderpet.com", "301 123 4567", "Calle 1 # 2 - 3", UserType.Admin);
            await CheckUserAsync("101012", "Stewar", "Marin", "stewartubb@guarderpet.com", "302 123 4567", "Calle 1 # 2 - 3", UserType.User);
            await CheckUserAsync("101013", "Zulu", "El Profe", "zulu@guarderpet.com", "303 123 4567", "Calle 1 # 2 - 3", UserType.User);
            await CheckUserAsync("101014", "Megan", "Foss", "lafoss@guarderpet.com", "304 123 4567", "Calle 1 # 2 - 3", UserType.Carer);
        }

        private async Task CheckUserAsync(string document, string firstName, string lastName, string email, string phoneNumber, string address, UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Document = document,
                    PhoneNumber = phoneNumber,
                    Address = address,
                    DocumentType = _context.DocumentTypes.FirstOrDefault(x => x.Type == "Cédula"),
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                //await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                //string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                //await _userHelper.ConfirmEmailAsync(user, token);
            }
        }

        private async Task CheckDocumentTypesAsync()
        {
            if (!_context.DocumentTypes.Any())
            {
                _context.DocumentTypes.Add(new DocumentType { Type = "Cédula" });
                _context.DocumentTypes.Add(new DocumentType { Type = "Tarjeta de Identidad" });
                _context.DocumentTypes.Add(new DocumentType { Type = "NIT" });
                _context.DocumentTypes.Add(new DocumentType { Type = "Pasaporte" });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckRolesAsycn()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
            await _userHelper.CheckRoleAsync(UserType.Carer.ToString());
        }

    }
}
