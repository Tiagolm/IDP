using Application.ViewModels.PhoneContact;
using Domain.Core;

namespace Application.ViewModels.Contact
{
    public class ContactResponse : Entity
    {
        public string Name { get; set; }
        public IEnumerable<PhoneContactResponse> Phones { get; set; } = new List<PhoneContactResponse>();
    }
}