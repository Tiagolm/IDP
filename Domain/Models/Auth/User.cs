using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Auth
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }
    }
}
