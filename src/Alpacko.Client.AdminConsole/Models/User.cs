using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Alpacko.Client.AdminConsole.Models
{
    public class User
    {
        public int? Id { get; }
        public string Email { get; }
        public string Role { get; }
        public IEnumerable<Claim> Claims { get; }
        public string Token { get; }

        public User(IEnumerable<Claim> claims, string token)
        {
            Claims = claims;
            Token = token;

            Id = Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value.ToInt32();
            Email = Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            Role = Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        }
    }
}