using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alpacko.API.Models;
using Microsoft.AspNetCore.Authorization;
using Alpacko = Alpacko.API.Models.Results;

namespace Alpacko.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisteredPackagesController : ControllerBase
    {
        private readonly AlpackoDatabaseContext _context;

        public RegisteredPackagesController(AlpackoDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/RegisteredPackages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegisteredPackage>>> GetRegisteredPackage()
        {
            return await _context.RegisteredPackage.ToListAsync();
        }

        // GET: api/RegisteredPackages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegisteredPackage>> GetRegisteredPackage(int id)
        {
            var registeredPackage = await _context.RegisteredPackage.FindAsync(id);

            if (registeredPackage == null)
            {
                return NotFound();
            }

            return registeredPackage;
        }

        //// PUT: api/RegisteredPackages/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRegisteredPackage(int id, RegisteredPackage registeredPackage)
        //{
        //    if (id != registeredPackage.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(registeredPackage).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RegisteredPackageExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/RegisteredPackages
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<RegisteredPackage>> PostRegisteredPackage(RegisteredPackage registeredPackage)
        {
            int id = User.GetLoggedInUserId<int>();
            RegisteredPackage alreadyRegisteredPackage = await _context.RegisteredPackage.FirstOrDefaultAsync(rp => rp.PackageId == registeredPackage.PackageId);

            if (alreadyRegisteredPackage != null)
                return BadRequest(new Alpacko::RegisteredPackageResult { Successful = false, ErrorMessage = "This package is already registered." });

            _context.RegisteredPackage.Add(registeredPackage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegisteredPackage", new { id = registeredPackage.Id }, new Alpacko::RegisteredPackageResult { Successful = true });
        }

        //// DELETE: api/RegisteredPackages/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<RegisteredPackage>> DeleteRegisteredPackage(int id)
        //{
        //    var registeredPackage = await _context.RegisteredPackage.FindAsync(id);
        //    if (registeredPackage == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.RegisteredPackage.Remove(registeredPackage);
        //    await _context.SaveChangesAsync();

        //    return registeredPackage;
        //}

        private bool RegisteredPackageExists(int id)
        {
            return _context.RegisteredPackage.Any(e => e.Id == id);
        }
    }
}
