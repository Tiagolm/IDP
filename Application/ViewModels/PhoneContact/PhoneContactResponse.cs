using Application.ViewModels.PhoneContactType;
using Domain.Core;

namespace Application.ViewModels.PhoneContact
{
    public class PhoneContactResponse : Entity
    {
        public PhoneContactTypeResponse PhoneContactType { get; set; }
        public string Description { get; set; }
        public string FormattedPhone { get; set; }
        public int Ddd { get; set; }
        public string Phone { get; set; }
    }
}