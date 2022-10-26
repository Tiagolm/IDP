using Application.ViewModels.PhoneContact;
using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Contact
{
    internal class ContactResponse : Entity
    {
        public string Name { get; set; }
        public IEnumerable<PhoneContactResponse> Phones { get; set; } = new List<PhoneContactResponse>();
    }
}
