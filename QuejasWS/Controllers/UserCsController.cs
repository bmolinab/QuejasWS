using System;
using System.Collections.Generic;
using System.IO;
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
        [Route("Login")]
        public async Task<Response> LoginUserCs([FromBody] UserC userC)
        {
            try
            {
                var existeUsuario = _context.UserC.
                               Where(u => u.UserName == userC.UserName && u.Password == userC.Password)
                               .FirstOrDefault();
                if(existeUsuario!=null)
                {
                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Acceso completo",
                        Result = existeUsuario
                    };
                }
                else
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Contraseña o Usuario Incorrecto",
                        Result = null
                    };
                }
            }
            catch
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Ocurrio un error inesperado",
                    Result = null
                };
            }
        }



        [HttpPost]
        [Route ("UploadProfilePicture")]
        public async Task<IActionResult> PostProfilePicture(IFormFile file)
        {
          

            return Ok("File uploaded successfully"); 
        }

         


        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> RegistrarUserC([FromBody] UserC userC)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _context.UserC.Add(userC);
            await _context.SaveChangesAsync();
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