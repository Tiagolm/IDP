using Application.ViewModels.User;

namespace Application.ViewModels.Contact
{
    public class AdminContactResponse : ContactResponse
    {
        public UserResponse User { get; set; }
    }
}