using Project_RestApi.Models;

namespace Project_RestApi.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}
