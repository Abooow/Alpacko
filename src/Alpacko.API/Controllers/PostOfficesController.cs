using Alpacko.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpacko.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostOfficesController : ControllerBase
    {
        private readonly AlpackoDatabaseContext _context;

        public PostOfficesController(AlpackoDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/PostOffices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostOffice>>> GetPostOffice()
        {
            return await _context.PostOffice.ToListAsync();
        }

        // GET: api/PostOffices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostOffice>> GetPostOffice(int id)
        {
            PostOffice postOffice = await _context.PostOffice.FindAsync(id);

            if (postOffice == null)
            {
                return NotFound();
            }

            return postOffice;
        }

        // PUT: api/PostOffices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostOffice(int id, PostOffice postOffice)
        {
            if (id != postOffice.Id)
            {
                return BadRequest();
            }

            _context.Entry(postOffice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostOfficeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PostOffices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PostOffice>> PostPostOffice(PostOffice postOffice)
        {
            _context.PostOffice.Add(postOffice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPostOffice", new { id = postOffice.Id }, postOffice);
        }

        // DELETE: api/PostOffices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PostOffice>> DeletePostOffice(int id)
        {
            PostOffice postOffice = await _context.PostOffice.FindAsync(id);
            if (postOffice == null)
            {
                return NotFound();
            }

            _context.PostOffice.Remove(postOffice);
            await _context.SaveChangesAsync();

            return postOffice;
        }

        private bool PostOfficeExists(int id)
        {
            return _context.PostOffice.Any(e => e.Id == id);
        }
    }
}