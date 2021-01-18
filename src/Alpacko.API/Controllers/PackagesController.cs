using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alpacko.API.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

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
        public async Task<ActionResult<IEnumerable<Package>>> GetPackages([FromQuery] int skip, [FromQuery] int? take)
        {
            Package[] packages = await _context.Package.Skip(skip).ToArrayAsync();
            if (take != null && take >= 0)
                packages = packages.Take(take.Value).ToArray();

            return packages;
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
