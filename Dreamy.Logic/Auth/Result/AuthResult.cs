using Dreamy.Domain.Shared.Dtos.Auth;

namespace Dreamy.Logic.Auth.Result
{
    public class AuthResult
    {
        public string Token { get; set; }
        public bool IsAuthSuccessful { get; set; }
        public AuthEntity User { get; set; }
    }
}
