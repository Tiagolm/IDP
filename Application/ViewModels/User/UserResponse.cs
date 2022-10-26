using Application.ViewModels.UserRole;
using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.User
{
    internal class UserResponse : Entity
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public UserRoleResponse UserRole { get; set; }
    }
}
