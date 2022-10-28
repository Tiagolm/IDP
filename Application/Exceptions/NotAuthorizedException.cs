using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException() : base("Usuário logado não autorizado.")
        {
        }
    }
}
