using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Auth
{
    public class UserRole : Enumeration
    {
        public static UserRole Admin = new UserRole(1, "admin");
        public static UserRole Comum = new UserRole(2, "comum");

        private UserRole() { }
        public UserRole(int id, string nome) : base(id, nome) { }
    }
}
