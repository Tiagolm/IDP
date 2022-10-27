namespace Application.ViewModels.Contact
{
    public class AdminContactRequest
    {
        public int UserId { get; set; }
        public ContactRequest Contact { get; set; }
    }
}