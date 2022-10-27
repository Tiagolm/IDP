using Application.ViewModels.Login;

namespace Application.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponse> Authenticate(LoginRequest model);
    }
}