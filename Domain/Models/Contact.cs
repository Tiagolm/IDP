using Domain.Core;
using Domain.Models.Auth;

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