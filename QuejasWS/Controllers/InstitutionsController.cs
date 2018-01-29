using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuejasWS.Models;

namespace QuejasWS.Controllers
{
    [Produces("application/json")]
    [Route("api/Institutions")]
    public class InstitutionsController : Controller
    {
        private readonly QuejasDBContext _context;

        public InstitutionsController(QuejasDBContext context)
        {
            _context = context;
        }

        // GET: api/Institutions
        [HttpGet]
        public IEnumerable<Institution> GetInstitution()
        {
            return _context.Institution;
        }

        // GET: api/Institutions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstitution([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var institution = await _context.Institution.SingleOrDefaultAsync(m => m.IdInstitution == id);

            if (institution == null)
            {
                return NotFound();
            }

            return Ok(institution);
        }

        // PUT: api/Institutions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstitution([FromRoute] string id, [FromBody] Institution institution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != institution.IdInstitution)
            {
                return BadRequest();
            }

            _context.Entry(institution).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstitutionExists(id))
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

        // POST: api/Institutions
        [HttpPost]
        public async Task<IActionResult> PostInstitution([FromBody] Institution institution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Institution.Add(institution);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InstitutionExists(institution.IdInstitution))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInstitution", new { id = institution.IdInstitution }, institution);
        }

        // DELETE: api/Institutions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstitution([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var institution = await _context.Institution.SingleOrDefaultAsync(m => m.IdInstitution == id);
            if (institution == null)
            {
                return NotFound();
            }

            _context.Institution.Remove(institution);
            await _context.SaveChangesAsync();

            return Ok(institution);
        }

        private bool InstitutionExists(string id)
        {
            return _context.Institution.Any(e => e.IdInstitution == id);
        }
    }
}