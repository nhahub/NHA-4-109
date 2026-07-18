using DataAccessLayer.Classes;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAuthRepository
    {
        (string AccessToken, string RefreshToken, int ExpiresIn) RegisterAdmin(Admin admin, string plainPassword);
        (string AccessToken, string RefreshToken, int ExpiresIn) RegisterOwner(Owner owner, string plainPassword);
        (string AccessToken, string RefreshToken, int ExpiresIn) RegisterTenant(Tentant tenant, string plainPassword);
        (string AccessToken, string RefreshToken, int ExpiresIn, string Role) Login(string email, string password);
    }
}