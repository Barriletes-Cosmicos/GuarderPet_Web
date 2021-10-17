using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GuarderPet.API.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboDocumentTypes();
        IEnumerable<SelectListItem> GetComboPetServices();
        IEnumerable<SelectListItem> GetComboPetTypes();
        IEnumerable<SelectListItem> GetComboBreeds();
    }
}
