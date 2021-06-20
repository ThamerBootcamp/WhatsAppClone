using System;
using System.IdentityModel.Tokens.Jwt;

namespace WhatsAppApi.Helpers
{
   public interface IJwtService
   {
        string Generate(int id);
        JwtSecurityToken Verify(string jwt);
    }
}
