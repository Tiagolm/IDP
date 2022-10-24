using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        public AuthService(IHttpContextAccessor httpContextAccessor = null)
        {
            if (httpContextAccessor != null)
                SetUserLogged(httpContextAccessor.HttpContext.User);
        }

        public IAuthUser LoggedUser { get; private set; }

        public void SetUserLogged(ClaimsPrincipal principal)
        {
            if (principal.Identity.IsAuthenticated)
            {
                var userIdClaim = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
                var nameClaim = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);

                LoggedUser = new AuthUser
                {
                    Id = int.Parse(userIdClaim.Value),
                    Name = nameClaim.Value,
                };
            }
        }
    }
}