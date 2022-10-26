using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SearchParams
{
    public class AdminQueryParam : ContactQueryParam
    {
        public int? UserId { get; set; }
    }
}
