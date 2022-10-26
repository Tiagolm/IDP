using Domain.Core;
using Domain.Models.Auth;

namespace Domain.Models
{
    public class Contact : Entity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public IEnumerable<PhoneContact> Phones { get; set; }
    }
}