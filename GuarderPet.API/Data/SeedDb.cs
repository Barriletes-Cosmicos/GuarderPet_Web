using GuarderPet.API.Data.Entities;
using GuarderPet.API.Helpers;
using GuarderPet.Common.Enums;
using System;
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
            await CheckRolesAsync();
            await CheckPetTypesAsync();
            await CheckBreedsAsync();
            await CheckDocumentTypesAsync();
            await CheckPetServicesAsync();
            await CheckPlacesAsync();
            await CheckUserAsync("101010", "Santiago", "Osorio", "osorio@guarderpet.com", "300 123 4567", "Calle 1 # 2 - 3", UserType.Carer, "Guarderia 1");
            await CheckUserAsync("101011", "Lucas", "Giraldo", "lukitag@guarderpet.com", "301 123 4567", "Calle 1 # 2 - 3", UserType.Carer, "Guarderia 2");
            await CheckUserAsync("101012", "Stewar", "Marin", "stewarm@guarderpet.com", "302 123 4567", "Calle 1 # 2 - 3", UserType.Carer, "Guarderia 3");
            await CheckUserAsync("101013", "Zulu", "El Profe", "zulu@guarderpet.com", "303 123 4567", "Calle 1 # 2 - 3", UserType.Carer, "Guarderia 1");
            await CheckUserAsync("101014", "Andres", "Arango", "aa@yopmail.com", "303 123 4567", "Calle 1 # 2 - 3", UserType.User, "Guarderia 3");
            await CheckUserAsync("101015", "Caterine", "Caminos", "catcam@yopmail.com", "303 123 4567", "Calle 1 # 2 - 3", UserType.User, "Guarderia 1");
            await CheckUserAsync("101016", "Julio", "Cesar", "julces@yopmail.com", "304 123 4567", "Calle 1 # 2 - 3", UserType.User, "Guarderia 2");
            await CheckUserAsync("101017", "Tulio", "Recomienda", "tulrec@yopmail.com", "304 123 4567", "Calle 1 # 2 - 3", UserType.User, "Guarderia 1");
            await CheckUserAsync("101018", "Pablo", "Neruda", "pabner@yopmail.com", "304 123 4567", "Calle 1 # 2 - 3", UserType.User, "Guarderia 3");
           
        }

       

        private async Task CheckPlacesAsync()
        {
            
            _context.Places.Add(new Place
            {
                PlaceName = "Guarderia 1",
                Direction = "Calle 3 #4 - 5",
               
            });

            _context.Places.Add(new Place
            {
                PlaceName = "Guarderia 2",
                Direction = "Calle 6 #7 - 9",

            });
            _context.Places.Add(new Place
            {
                PlaceName = "Guarderia 3",
                Direction = "Calle 10 #11 - 12",

            });

            await _context.SaveChangesAsync();
        }

        private async Task CheckUserAsync(string document, string firstName, string lastName, string email, string phoneNumber, string address, UserType userType, string place)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    Address = address,
                    Document = document,
                    DocumentType = _context.DocumentTypes.FirstOrDefault(x => x.Type == "Cédula"),
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    UserName = email,
                    UserType = userType,
                    Place = _context.Places.FirstOrDefault(x => x.PlaceName == place)
                };

                await _userHelper.AddUserAsync(user, "Pruebas123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
            await _userHelper.CheckRoleAsync(UserType.Carer.ToString());
        }
        private async Task CheckBreedsAsync()
        {
            if (!_context.Breeds.Any())
            {
                _context.Breeds.Add(new Breed { 
                    BreedTittle = "Pincher", 
                    PetType = _context.PetTypes.FirstOrDefault(x => x.Type == "Perro") 
                });
                _context.Breeds.Add(new Breed { 
                    BreedTittle = "Pastor Aleman",
                    PetType = _context.PetTypes.FirstOrDefault(x => x.Type == "Perro")
                });
                _context.Breeds.Add(new Breed { 
                    BreedTittle = "Chihuahua",
                    PetType = _context.PetTypes.FirstOrDefault(x => x.Type == "Perro")
                });
                _context.Breeds.Add(new Breed { 
                    BreedTittle = "Husky",
                    PetType = _context.PetTypes.FirstOrDefault(x => x.Type == "Perro")
                });
                _context.Breeds.Add(new Breed { 
                    BreedTittle = "Bulldog",
                    PetType = _context.PetTypes.FirstOrDefault(x => x.Type == "Perro")
                });
                _context.Breeds.Add(new Breed { 
                    BreedTittle = "Dobermann",
                    PetType = _context.PetTypes.FirstOrDefault(x => x.Type == "Perro")
                });
                _context.Breeds.Add(new Breed { 
                    BreedTittle = "Poodle",
                    PetType = _context.PetTypes.FirstOrDefault(x => x.Type == "Perro")
                });
                _context.Breeds.Add(new Breed { 
                    BreedTittle = "Persa",
                    PetType = _context.PetTypes.FirstOrDefault(x => x.Type == "Gato")
                });
                _context.Breeds.Add(new Breed { 
                    BreedTittle = "Angora",
                    PetType = _context.PetTypes.FirstOrDefault(x => x.Type == "Gato")
                });
                _context.Breeds.Add(new Breed { 
                    BreedTittle = "Border Collie",
                    PetType = _context.PetTypes.FirstOrDefault(x => x.Type == "Gato")
                });
                _context.Breeds.Add(new Breed { 
                    BreedTittle = "Siamés",
                    PetType = _context.PetTypes.FirstOrDefault(x => x.Type == "Gato")
                });
                _context.Breeds.Add(new Breed { 
                    BreedTittle = "Snowshoe",
                    PetType = _context.PetTypes.FirstOrDefault(x => x.Type == "Gato")
                });
                _context.Breeds.Add(new Breed { 
                    BreedTittle = "Ragdoll",
                    PetType = _context.PetTypes.FirstOrDefault(x => x.Type == "Gato")
                });
                _context.Breeds.Add(new Breed { 
                    BreedTittle = "Bengalí",
                    PetType = _context.PetTypes.FirstOrDefault(x => x.Type == "Gato")
                });
                _context.Breeds.Add(new Breed { 
                    BreedTittle = "Mediterránea",
                    PetType = _context.PetTypes.FirstOrDefault(x => x.Type == "Tortuga")
                });
                _context.Breeds.Add(new Breed { 
                    BreedTittle = "Rusa",
                    PetType = _context.PetTypes.FirstOrDefault(x => x.Type == "Tortuga")
                });
                await _context.SaveChangesAsync();
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

        private async Task CheckPetServicesAsync()
        {
            if (!_context.PetServices.Any())
            {
                _context.PetServices.Add(new PetService { Price = 10000, ServiceDetail = "Cuidar" });
                _context.PetServices.Add(new PetService { Price = 7000, ServiceDetail = "Baño" });
                _context.PetServices.Add(new PetService { Price = 10000, ServiceDetail = "Corte de uñas" });
                _context.PetServices.Add(new PetService { Price = 40000, ServiceDetail = "Desparasitación" });
                _context.PetServices.Add(new PetService { Price = 60000, ServiceDetail = "Vacunacion" });
                _context.PetServices.Add(new PetService { Price = 5000, ServiceDetail = "Pasear" });
                _context.PetServices.Add(new PetService { Price = 10000, ServiceDetail = "Limpieza dental" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckPetTypesAsync()
        {
            if (!_context.PetTypes.Any())
            {
                _context.PetTypes.Add(new PetType { Type = "Perro" });
                _context.PetTypes.Add(new PetType { Type = "Gato" });
                _context.PetTypes.Add(new PetType { Type = "Tortuga"});
                await _context.SaveChangesAsync();
            }
        }
    }
}
