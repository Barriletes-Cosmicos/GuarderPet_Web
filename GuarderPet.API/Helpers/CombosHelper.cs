using GuarderPet.API.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace GuarderPet.API.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboBreeds()
        {
            List<SelectListItem> list = _context.Breeds.Select(x => new SelectListItem
            {
                Text = x.BreedTittle,
                Value = $"{x.Id }",
            }).OrderBy(x => x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una marca...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboPetServices()
        {
            List<SelectListItem> list = _context.PetServices.Select(x => new SelectListItem
            {
                Text = x.ServiceDetail,
                Value = $"{x.Id }",
            }).OrderBy(x => x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un procedimiento...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboDocumentTypes()
        {
            List<SelectListItem> list = _context.DocumentTypes.Select(x => new SelectListItem
            {
                Text = x.Type,
                Value = $"{x.Id }",
            }).OrderBy(x => x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un tipo de documento...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboPetTypes()
        {
            List<SelectListItem> list = _context.PetTypes.Select(x => new SelectListItem
            {
                Text = x.Type,
                Value = $"{x.Id }",
            }).OrderBy(x => x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un tipo de mascota...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboPlaces()
        {
            List<SelectListItem> list = _context.Places.Select(x => new SelectListItem
            {
                Text = x.PlaceName,
                Value = $"{x.Id}",
            }).OrderBy(x => x.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un lugar...]",
                Value = "0"
            });

            return list;
        }
    }
}
