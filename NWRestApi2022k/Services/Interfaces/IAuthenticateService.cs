using NWRestApi2022k.Models;

namespace NWRestApi2022k.Services.Interfaces
{
    public interface IAuthenticateService
    {
        LoggedUser Authenticate(string username, string password);
    }
}
