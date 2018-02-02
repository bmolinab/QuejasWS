using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment _hostingEnvironment = Constants._env;

      

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
        public async Task<Response> PostProfilePicture([FromBody]FileRequest file)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "error de modelo",
                    Result = BadRequest(ModelState)
                };
            }

            try
            {
                string folder = "ProfilePictures";
                string extension = ".jpg";
                    var stream = new MemoryStream(file.File);
                    var a = string.Format("{0}/{1}{2}", folder, file.Name, extension);
                    var targetDirectory = Path.Combine(_hostingEnvironment.ContentRootPath, a);
                    using (var fileStream = new FileStream(targetDirectory, FileMode.Create, FileAccess.Write))
                    {
                        await stream.CopyToAsync(fileStream);
                    }

                return new Response
                {
                    IsSuccess = true,
                    Message = "La foto se subio con exito",
                    Result = Ok(),
                };


            }
            catch(Exception ex)
            {
                return new Response
                {
                    IsSuccess=false,
                    Message= "Sucedio un error con el archivo",
                    Result=  _hostingEnvironment.WebRootPath +" brian"+ ex,
                };
            }

        }
         
        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> RegistrarUserC([FromBody] UserC userC)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(userC.FacebookId!=null & userC.FacebookId!="")
            {
                var existeUsuario = _context.UserC.
                              Where(u => u.FacebookId == userC.FacebookId)
                              .FirstOrDefault();
                if (existeUsuario != null)
                {
                    return Ok(existeUsuario);
                }
                userC.UserName = "FB_" + userC.FacebookId;
            }

            if (userC.TwitterId != null & userC.TwitterId != "")
            {
                var existeUsuario = _context.UserC.
                              Where(u => u.TwitterId == userC.TwitterId)
                              .FirstOrDefault();
                if (existeUsuario != null)
                {
                    return Ok(existeUsuario);
                }
                
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