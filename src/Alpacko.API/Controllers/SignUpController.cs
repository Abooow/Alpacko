using Alpacko.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alpacko = Alpacko.API.Models.Results;

namespace Alpacko.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AlpackoDatabaseContext _context;

        public SignUpController(IConfiguration configuration,
                               AlpackoDatabaseContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            if (await UserAlreadyExists(user))
                return BadRequest(new Alpacko::SignUpResult { Successful = false, ErrorMessage = "A user with this email already exists." });

            SaltUserPassword(user);
            user.UserRoleId = await GetRoleId("User");

            FixUserName(user);

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new Alpacko::SignUpResult { Successful = true, ErrorMessage = "" });
        }

        // POST: api/signup/5
        [HttpPost("{userRoleId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SignUp([FromBody] User user, int userRoleId, [FromQuery] int? postOfficeId)
        {
            if (await UserAlreadyExists(user))
                return BadRequest(new Alpacko::SignUpResult { Successful = false, ErrorMessage = "A user with this email already exists." });

            SaltUserPassword(user);
            user.UserRoleId = userRoleId;

            user.PostOfficeId = postOfficeId;

            FixUserName(user);

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new Alpacko::SignUpResult { Successful = true, ErrorMessage = "" });
        }

        private async Task<bool> UserAlreadyExists(User user)
        {
            user.Email = user.Email.ToLower().Trim();
            User foundUser = await _context.User.FirstOrDefaultAsync(u => u.Email == user.Email);

            return foundUser != null;
        }

        private void SaltUserPassword(User user)
        {
            string salt = KeyGenerator.GetUniqueKey(64);
            string saltedPassword = Utils.ComputeSha256Hash(string.Concat(user.Password, salt));
            user.Password = saltedPassword;
            user.Salt = salt;
        }

        // TODO: Change method name!
        private void FixUserName(User user)
        {
            user.FirstName = user.FirstName.UppercaseFirst();
            user.LastName = user.LastName.UppercaseFirst();
        }

        private async Task<int> GetRoleId(string roleName)
        {
            UserRole userRole = await _context.UserRole.FirstOrDefaultAsync(role => role.Name == roleName);
            return userRole.Id;
        }
    }
}