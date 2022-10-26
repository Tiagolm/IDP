using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SearchParams
{
    public class ContactQueryParam
    {
        public int? IdContato { get; set; }
        public string ContactName { get; set; }
        public int? Ddd { get; set; }
        public string Number { get; set; }
    }
}
