using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PhoneContactType : Enumeration
    {
        public static PhoneContactType Casa = new(1, "Casa");
        public static PhoneContactType Comercial = new(2, "Comercial");
        public static PhoneContactType Celular = new(3, "Celular");

        public PhoneContactType() { }
        public PhoneContactType(int id, string name) : base(id, name) { }
    }
}
