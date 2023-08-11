namespace TekhneCafe.Core.DTOs.Authentication
{
    public class JwtResponse
    {
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
