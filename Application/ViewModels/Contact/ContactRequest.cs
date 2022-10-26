using Application.ViewModels.PhoneContact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Contact
{
    internal class ContactRequest
    {
        public string Name { get; set; }
        public IEnumerable<PhoneContactRequest> Phones { get; set; } = new List<PhoneContactRequest>();
    }
}
