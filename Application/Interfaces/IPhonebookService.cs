using Application.SearchParams;
using Application.ViewModels.Contact;
using Application.ViewModels.PhoneContact;
using Application.ViewModels.PhoneContactType;
using Domain.Core;

namespace Application.Interfaces
{
    public interface IPhonebookService
    {
        Task<PaginationResult<ContactResponse>> SaerchContacts(ContactQueryParam viewModel);

        Task<IEnumerable<PhoneContactResponse>> SearchPhones(ContactQueryParam viewModel);

        IEnumerable<PhoneContactTypeResponse> PhoneContactTypes();

        Task<ContactResponse> GetContact(int id);

        Task<ContactResponse> AddContact(ContactRequest contactViewModel);

        Task<ContactResponse> UpdateContact(int id, ContactRequest contactViewModel);

        Task RemoveContact(int id);
    }
}