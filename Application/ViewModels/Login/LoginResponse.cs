namespace Application.ViewModels.Login
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public Domain.Models.Auth.UserRole Role { get; set; }
        public string Token { get; set; }

        public LoginResponse(Domain.Models.Auth.User user, string token)
        {
            Id = user.Id;
            Name = user.Name;
            Username = user.Username;
            Role = user.UserRole;
            Token = token;
        }
    }
}