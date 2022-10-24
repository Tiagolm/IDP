using System.Security.Claims;

namespace Domain.Interfaces
{
    public interface IAuthService
    {
        void SetUserLogged(ClaimsPrincipal principal);

        IAuthUser LoggedUser { get; }
    }
}