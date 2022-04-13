using NWRestApi2022k.Models;

namespace NWRestApi2022k.Services.Interfaces
{
    public interface IAuthenticateService
    {
        User Authenticate(string username, string password);
    }
}
