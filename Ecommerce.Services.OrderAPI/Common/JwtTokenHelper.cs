using System.IdentityModel.Tokens.Jwt;

namespace Ecommerce.Services.OrderAPI.Common
{
    public class JwtTokenHelper
    {
        public static JwtPayload GetJwtPayload(string jwtToken)
        {
            if (jwtToken != null && jwtToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                var handler = new JwtSecurityTokenHandler();
                var token = jwtToken.Substring("Bearer ".Length).Trim();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                return jsonToken.Payload;
            }
            else return null;
        }

    }
}
