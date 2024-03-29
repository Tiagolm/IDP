﻿using Domain.Core;

namespace Domain.Models.Auth
{
    public class UserRole : Enumeration
    {
        public static UserRole Admin = new UserRole(1, "admin");
        public static UserRole Comum = new UserRole(2, "comum");

        private UserRole()
        { }

        public UserRole(int id, string nome) : base(id, nome)
        {
        }
    }
}