using Domain.Core;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IContactRepository : IBaseRepository<Contact>, IQueryParamRepository<Contact>
    {
        Task<IEnumerable<Contact>> Search(int? idContato, string nomeContato, int? ddd, string numero);

        Task<IEnumerable<Contact>> SearchPhones(string phone);

        Task<IEnumerable<Contact>> SearchPhones(int ddd);

        Task<bool> PhoneExists(string formattedPhone, CancellationToken token);
    }
}