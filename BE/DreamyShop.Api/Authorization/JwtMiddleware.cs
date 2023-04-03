using DreamyShop.Logic.Auth.Security;

namespace DreamyShop.Api.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, AccessToken tokenService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                AttachUserToContext(context, tokenService, token);
            }

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, AccessToken tokenService, string token)
        {
            try
            {
                var auth = tokenService.ParseJwtToken(token);
                context.Items["Token"] = token;
                context.Items["Auth"] = auth;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
