using Domain.Interfaces;

namespace Infrastructure.Auth
{
    public class AuthUser : IAuthUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}