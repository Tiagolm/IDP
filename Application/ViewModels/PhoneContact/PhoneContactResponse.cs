using Application.ViewModels.PhoneContactType;
using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.PhoneContact
{
    internal class PhoneContactResponse : Entity
    {
        public PhoneContactTypeResponse PhoneContactType { get; set; }
        public string Description { get; set; }
        public string FormattedPhone { get; set; }
        public int Ddd { get; set; }
        public string Phone { get; set; }
    }
}
