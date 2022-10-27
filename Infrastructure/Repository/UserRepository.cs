using Domain.Interfaces;
using Domain.Models.Auth;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}