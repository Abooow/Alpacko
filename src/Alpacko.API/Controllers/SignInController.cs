using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Alpacko.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Alpacko.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AlpackoDatabaseContext _context;

        public SignInController(IConfiguration configuration,
                               AlpackoDatabaseContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInModel login)
        {
            User authenticatedUser = await AuthenticateUser(login.Email, login.Password);

            if (authenticatedUser is null)
                return BadRequest(new SignInResultModel { Successful = false, ErrorMessage = "Email or password are invalid." });

            string token = await GenerateJSONWebToken(authenticatedUser);

            return Ok(new SignInResultModel { Successful = true, Token = token });
        }


        private async Task<User> AuthenticateUser(string email, string password)
        {
            User user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);

            if (user is null)
                return null;

            string hashedPassword = Utils.ComputeSha256Hash(string.Concat(password, user.Salt));
            if (!user.Password.Equals(hashedPassword))
                return null;

            return user;
        }

        private async Task<string> GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            string userRole = await GetUserRoleAsync(user.Id);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, userRole)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(Convert.ToInt32(_configuration["Jwt:ExpiryInDays"])),
                signingCredentials: credentials);

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedToken;
        }

        private async Task<string> GetUserRoleAsync(int userId)
        {
            UserRole userRole = await _context.UserRole.FirstOrDefaultAsync(role => role.Id == userId);
            return userRole.Name;
        }
    }
}
