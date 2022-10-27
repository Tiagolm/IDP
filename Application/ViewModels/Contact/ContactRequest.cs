using Application.ViewModels.PhoneContact;

namespace Application.ViewModels.Contact
{
    public class ContactRequest
    {
        public string Name { get; set; }
        public IEnumerable<PhoneContactRequest> Phones { get; set; } = new List<PhoneContactRequest>();
    }
}