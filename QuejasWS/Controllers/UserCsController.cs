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
    [Route("api/UserCs")]
    public class UserCsController : Controller
    {
        private readonly QuejasDBContext _context;

        public UserCsController(QuejasDBContext context)
        {
            _context = context;
        }

        // GET: api/UserCs
        [HttpGet]
        public IEnumerable<UserC> GetUserC()
        {
            return _context.UserC;
        }

        // GET: api/UserCs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserC([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userC = await _context.UserC.SingleOrDefaultAsync(m => m.IdUser == id);

            if (userC == null)
            {
                return NotFound();
            }

            return Ok(userC);
        }

        // PUT: api/UserCs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserC([FromRoute] int id, [FromBody] UserC userC)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userC.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(userC).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCExists(id))
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

        // POST: api/UserCs
        [HttpPost]
        public async Task<IActionResult> PostUserC([FromBody] UserC userC)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserC.Add(userC);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserC", new { id = userC.IdUser }, userC);
        }
        [HttpPost]
        public async Task<IActionResult> RegistrarUserC([FromBody] UserC userC)
        {
            return CreatedAtAction("GetUserC", new { id = userC.IdUser }, userC);
        }

        // DELETE: api/UserCs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserC([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userC = await _context.UserC.SingleOrDefaultAsync(m => m.IdUser == id);
            if (userC == null)
            {
                return NotFound();
            }

            _context.UserC.Remove(userC);
            await _context.SaveChangesAsync();

            return Ok(userC);
        }

        private bool UserCExists(int id)
        {
            return _context.UserC.Any(e => e.IdUser == id);
        }
    }
}