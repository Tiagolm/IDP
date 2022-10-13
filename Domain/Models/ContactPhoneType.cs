using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ContactPhoneType : Enumeration
    {
        public static ContactPhoneType Casa = new(1, "Casa");
        public static ContactPhoneType Comercial = new(2, "Comercial");
        public static ContactPhoneType Celular = new(3, "Celular");

        public ContactPhoneType() { }
        public ContactPhoneType(int id, string name) : base(id, name) { }
    }
}
