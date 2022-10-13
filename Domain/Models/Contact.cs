using Domain.Core;
using Domain.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Contact : Entity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string Nome { get; set; }
        public IEnumerable<PhoneContact> Telefones { get; set; }
    }
}
