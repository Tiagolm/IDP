using Domain.Core;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IContactRepository : IBaseRepository<Contact>, IQueryParamRepository<Contact>
    {
        Task<IEnumerable<Contact>> Search(int? contactId, string contactName, int? ddd, string number);

        Task<IEnumerable<Contact>> SearchPhones(string phone);

        Task<IEnumerable<Contact>> SearchPhones(int ddd);

        Task<bool> PhoneExists(string formattedPhone, CancellationToken token);
    }
}