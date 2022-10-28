namespace Application.ViewModels.PhoneContact
{
    public class PhoneContactRequest
    {
        public int PhoneContactTypeId { get; set; }
        public string Description { get; set; }
        public string FormattedPhone { get; set; }
    }
}