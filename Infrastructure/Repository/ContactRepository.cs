using Domain.Core;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public async Task<IEnumerable<Contact>> Search(int? contactId, string contactName, int? ddd, string number)
        {
            var query = Query
                .Include(x => x.Phones)
                .AsNoTracking();

            if (contactId.HasValue)
                query = query.Where(x => x.Id == contactId);

            if (!string.IsNullOrEmpty(contactName))
                query = query.Where(x => x.Name.Contains(contactName, StringComparison.InvariantCultureIgnoreCase));

            if (ddd.HasValue)
                query = query.Include(x => x.Phones.Where(tel => tel.Ddd == ddd));

            if (!string.IsNullOrEmpty(number))
                query = query.Include(x => x.Phones.Where(tel => tel.Phone.Contains(number, StringComparison.InvariantCultureIgnoreCase)));

            var list = await query.ToListAsync();

            return list.Where(x => x.Phones.Any());
        }

        public async Task<IEnumerable<Contact>> SearchPhones(string phone)
        {
            var query = Query
            .Include(x => x.Phones)
            .AsNoTracking().Include(x => x.Phones.Where(tel => tel.Phone.Equals(phone)));
            var phones = await query.ToListAsync();
            return phones.Where(x => x.Phones.Any());
        }

        public async Task<IEnumerable<Contact>> SearchPhones(int ddd)
        {
            var query = Query
            .Include(x => x.Phones)
            .AsNoTracking().Include(x => x.Phones.Where(tel => tel.Ddd == ddd));
            var phones = await query.ToListAsync();
            return phones.Where(x => x.Phones.Any());
        }

        public Task<bool> PhoneExists(string formattedPhone, CancellationToken token)
        {
            return Query.Include(x => x.Phones).SelectMany(x => x.Phones).AllAsync(x => x.FormattedPhone == formattedPhone, token);
        }

        public async Task<IEnumerable<Contact>> FilterAsync(IQueryParam<Contact> queryParam)
        {
            return await queryParam.ApplyFilter(Query).ToListAsync();
        }

        public async Task<PaginationResult<Contact>> PaginateAndFilterAsync(IPaginationQueryParam<Contact> queryParam)
        {
            var query = queryParam.ApplyFilter(Query);
            var total = await query.CountAsync();
            var list = await query.Skip(queryParam.Skip).Take(queryParam.Take).ToListAsync();

            return new PaginationResult<Contact>
            {
                Data = list,
                Take = queryParam.Take,
                Skip = queryParam.Skip,
                Total = total
            };
        }
    }
}