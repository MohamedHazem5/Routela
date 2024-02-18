using Routela.Models;


namespace Routela.Services.IServices
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);

    }
}