using GuarderPet.Common.Models;

namespace GuarderPet.API.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }
}
