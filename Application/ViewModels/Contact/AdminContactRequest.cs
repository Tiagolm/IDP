using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Contact
{
    internal class AdminContactRequest
    {
        public int UserId { get; set; }
        public ContactRequest Contact { get; set; }
    }
}
