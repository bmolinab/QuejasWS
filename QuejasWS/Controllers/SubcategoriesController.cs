using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuejasWS.Helpers;
using QuejasWS.Models;

namespace QuejasWS.Controllers
{
    [Produces("application/json")]
    [Route("api/Subcategories")]
    public class SubcategoriesController : Controller
    {
        private readonly QuejasDBContext _context;

        public SubcategoriesController(QuejasDBContext context)
        {
            _context = context;
        }

        // GET: api/Subcategories
        [HttpGet]
        public IEnumerable<Subcategory> GetSubcategory()
        {
            return _context.Subcategory;
        }



        // GET: api/Subcategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubcategory([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subcategory = await _context.Subcategory.SingleOrDefaultAsync(m => m.IdSubcategory == id);

            if (subcategory == null)
            {
                return NotFound();
            }

            return Ok(subcategory);
        }

        // PUT: api/Subcategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubcategory([FromRoute] string id, [FromBody] Subcategory subcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subcategory.IdSubcategory)
            {
                return BadRequest();
            }

            _context.Entry(subcategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubcategoryExists(id))
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

        // POST: api/Subcategories
        [HttpPost]
        public async Task<IActionResult> PostSubcategory([FromBody] Subcategory subcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Subcategory.Add(subcategory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SubcategoryExists(subcategory.IdSubcategory))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSubcategory", new { id = subcategory.IdSubcategory }, subcategory);
        }

        [Route("SubcategoryByCategory")]
        [HttpPost]
        public async Task<Response> GetSubcategoryByCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess=false,
                    Message="Error en el modelo",
                    Result=    BadRequest(ModelState)
                };
            }

            try
            {
                return new Response
                {
                    IsSuccess=true,
                    Message="Lista de Subcategoria",
                    Result = _context.Subcategory.Where(x => x.IdCategory == category.IdCategory).ToListAsync()
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Error ",
                    Result = ex
                };
            }
        }

        // DELETE: api/Subcategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubcategory([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subcategory = await _context.Subcategory.SingleOrDefaultAsync(m => m.IdSubcategory == id);
            if (subcategory == null)
            {
                return NotFound();
            }

            _context.Subcategory.Remove(subcategory);
            await _context.SaveChangesAsync();

            return Ok(subcategory);
        }

        private bool SubcategoryExists(string id)
        {
            return _context.Subcategory.Any(e => e.IdSubcategory == id);
        }
    }
}