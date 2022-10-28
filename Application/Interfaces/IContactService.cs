using Application.SearchParams;
using Application.ViewModels.Contact;

namespace Application.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<AdminContactResponse>> SearchContacts(AdminContactQueryParam viewModel);
        Task<AdminContactResponse> GetContact(int id);
        Task<AdminContactResponse> AddContact(AdminContactRequest contactViewModel);
        Task<AdminContactResponse> UpdateContact(int id, AdminContactRequest contactViewModel);
        Task DeleteContact(int id);
    }
}