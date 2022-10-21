using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PhoneContact : Entity
    {
        public int ContactId { get; set; }
        public virtual Contact Contato { get; set; }
        public string Description { get; set; }
        public int Ddd { get; set; }
        public string Phone { get; set; }
        public string FormatedPhone { get; set; }

        public int ContactPhoneTypeId { get; set; }
        public PhoneContactType TipoContatoTelefone { get; set; }
    }
}
