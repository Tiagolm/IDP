using Domain.Models.Auth;

namespace Infrastructure.Auth
{
    public interface IJwtGeneratorService
    {
        string GenerateToken(User user);
    }
}