using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.PhoneContact
{
    public class PhoneContactRequest
    {
        public int PhoneContactTypeIdId { get; set; }
        public string Description { get; set; }
        public string FormattedPhone { get; set; }
    }
}
