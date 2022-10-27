namespace Infrastructure.Auth
{
    public class JwtGeneratorOptions
    {
        public int ValidMinutes { get; set; }
        public string Secret { get; set; }
    }
}