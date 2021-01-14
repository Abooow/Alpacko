using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alpacko.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace Alpacko.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private const int maxPackageIdLenght = 10;
        private readonly AlpackoDatabaseContext _context;

        public PackagesController(AlpackoDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Packages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Package>>> GetPackages()
        {
            return await _context.Package.ToListAsync();
        }

        // GET: api/Packages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackage(string id)
        {
            var package = await _context.Package.FindAsync(id);

            if (package == null)
                return NotFound();

            return package;
        }

        // POST: api/Packages
        [HttpPost]
        public async Task<ActionResult<Package>> PostPackage(Package package)
        {
            string newId = KeyGenerator.GetUniqueKey(maxPackageIdLenght, "1234567890".ToCharArray());
            package.Id = newId;

            _context.Package.Add(package);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackage", new { id = package.Id }, package);
        }
    }
}
