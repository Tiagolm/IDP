using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.QueryParam
{
    public abstract class PaginationQueryParamBase<T> : 
        QueryParamBase<T>, IPaginationQueryParam<T> where T : Entity
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
