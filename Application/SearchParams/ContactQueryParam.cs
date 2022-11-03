using Domain.Models;
using Infrastructure.QueryParam;
using Microsoft.EntityFrameworkCore;

namespace Application.SearchParams
{
    public class ContactQueryParam : PaginationQueryParamBase<Contact>
    {
        public int? ContactId { get; set; }
        public string? ContactName { get; set; }
        public int? Ddd { get; set; }
        public string? Number { get; set; }

        public override void Filter()
        {
            AddQuery(x => x
            .Include(c => c.User)
            .Include(c => c.Phones)
            .ThenInclude(c => c.PhoneContactType));


            if (ContactId.HasValue)
                AddQuery(x => x.Where(c => c.Id == ContactId));

            if (!string.IsNullOrEmpty(ContactName))
                AddQuery(x => x.Where(c => EF.Functions.Like(c.Name, $"%{ContactName}%")));

            if (Ddd.HasValue)
                AddQuery(x => x.Include(c => c.Phones.Where(t => t.Ddd == Ddd)));

            if (!string.IsNullOrEmpty(Number))
                AddQuery(x => x.Include(c => c.Phones.Where(t => EF.Functions.Like(t.Phone, $"%{Number}%"))));
        }
    }
}