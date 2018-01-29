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
    [Route("api/SubcategoryInstitutions")]
    public class SubcategoryInstitutionsController : Controller
    {
        private readonly QuejasDBContext _context;

        public SubcategoryInstitutionsController(QuejasDBContext context)
        {
            _context = context;
        }

        // GET: api/SubcategoryInstitutions
        [HttpGet]
        public IEnumerable<SubcategoryInstitution> GetSubcategoryInstitution()
        {
            return _context.SubcategoryInstitution;
        }

        // GET: api/SubcategoryInstitutions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubcategoryInstitution([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subcategoryInstitution = await _context.SubcategoryInstitution.SingleOrDefaultAsync(m => m.IdSubCategoryInstitution == id);

            if (subcategoryInstitution == null)
            {
                return NotFound();
            }

            return Ok(subcategoryInstitution);
        }

        // PUT: api/SubcategoryInstitutions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubcategoryInstitution([FromRoute] string id, [FromBody] SubcategoryInstitution subcategoryInstitution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subcategoryInstitution.IdSubCategoryInstitution)
            {
                return BadRequest();
            }

            _context.Entry(subcategoryInstitution).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubcategoryInstitutionExists(id))
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

        // POST: api/SubcategoryInstitutions
        [HttpPost]
        public async Task<IActionResult> PostSubcategoryInstitution([FromBody] SubcategoryInstitution subcategoryInstitution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SubcategoryInstitution.Add(subcategoryInstitution);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SubcategoryInstitutionExists(subcategoryInstitution.IdSubCategoryInstitution))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSubcategoryInstitution", new { id = subcategoryInstitution.IdSubCategoryInstitution }, subcategoryInstitution);
        }

        // DELETE: api/SubcategoryInstitutions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubcategoryInstitution([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subcategoryInstitution = await _context.SubcategoryInstitution.SingleOrDefaultAsync(m => m.IdSubCategoryInstitution == id);
            if (subcategoryInstitution == null)
            {
                return NotFound();
            }

            _context.SubcategoryInstitution.Remove(subcategoryInstitution);
            await _context.SaveChangesAsync();

            return Ok(subcategoryInstitution);
        }

        private bool SubcategoryInstitutionExists(string id)
        {
            return _context.SubcategoryInstitution.Any(e => e.IdSubCategoryInstitution == id);
        }
    }
}