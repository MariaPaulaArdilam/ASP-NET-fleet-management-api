using FM_Api.Models;

namespace FM_Api.Interfaces
{
    public interface ITokenService
    {

        string GetToken(Users users);
    }
}
