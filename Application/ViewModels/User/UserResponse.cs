using Application.ViewModels.UserRole;
using Domain.Core;

namespace Application.ViewModels.User
{
    public class UserResponse : Entity
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public UserRoleResponse UserRole { get; set; }
    }
}