using Domain.Core;

namespace Domain.Models
{
    public class PhoneContact : Entity
    {
        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }
        public string Description { get; set; }
        public int Ddd { get; set; }
        public string Phone { get; set; }
        public string FormattedPhone { get; set; }

        public int PhoneContactTypeId { get; set; }
        public PhoneContactType PhoneContactType { get; set; }
    }
}