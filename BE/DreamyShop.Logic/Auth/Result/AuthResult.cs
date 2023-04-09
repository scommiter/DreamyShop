using DreamyShop.Domain.Shared.Dtos;

namespace DreamyShop.Logic.Auth.Result
{
    public class AuthResult
    {
        public string Token { get; set; }
        public bool IsAuthSuccessful { get; set; }
        public AuthEntity User { get; set; }
    }
}