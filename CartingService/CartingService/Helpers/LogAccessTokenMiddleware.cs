using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
namespace CartingService.API.Helpers
{
    public class LogAccessTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogAccessTokenMiddleware> _logger;

        public LogAccessTokenMiddleware(RequestDelegate next, ILogger<LogAccessTokenMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var accessToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var tokenContent = GetTokenContent(accessToken);
            _logger.LogInformation($"Access Token Details: {accessToken}");
            _logger.LogInformation($"Token content: {tokenContent}");
            await _next.Invoke(context);
        }

        private string GetTokenContent(string accessToken)
        {
            // Decodificamos el token utilizando la librería System.IdentityModel.Tokens.Jwt
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(accessToken);

            // Obtenemos el contenido del token
            string tokenContent = jwtToken.Payload.SerializeToJson();
            return tokenContent;
        }
    }
}
