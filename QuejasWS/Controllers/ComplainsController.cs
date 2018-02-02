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
    [Route("api/Complains")]
    public class ComplainsController : Controller
    {
        private readonly QuejasDBContext _context;
        private readonly IHostingEnvironment _hostingEnvironment = Constants._env;

        public ComplainsController(QuejasDBContext context)
        {
            _context = context;
        }

        // GET: api/Complains
        [HttpGet]
        public IEnumerable<Complain> GetComplain()
        {
            return _context.Complain;
        }

        [HttpPost]
        [Route("UploadPicture")]
        public async Task<Response> PostPicture([FromBody]FileRequest file)
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
                string folder = "ComplainPictures";
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
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Sucedio un error con el archivo",
                    Result = ex,
                };
            }

        }


        // GET: api/Complains/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComplain([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var complain = await _context.Complain.SingleOrDefaultAsync(m => m.IdComplain == id);

            if (complain == null)
            {
                return NotFound();
            }

            return Ok(complain);
        }

        // PUT: api/Complains/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComplain([FromRoute] string id, [FromBody] Complain complain)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != complain.IdComplain)
            {
                return BadRequest();
            }

            _context.Entry(complain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplainExists(id))
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

        // POST: api/Complains
        [HttpPost]
        [Route("PostComplain")]
        public async Task<Response> _PostComplain([FromBody] Complain complain)
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

            _context.Complain.Add(complain);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ComplainExists(complain.IdComplain))
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "error de modelo",
                        Result = StatusCodes.Status409Conflict
                     };
                }
                else
                {
                    throw;
                }
            }

            return new Response
            {
                IsSuccess = false,
                Message = "error de modelo",
                Result = CreatedAtAction("GetComplain", new { id = complain.IdComplain }, complain)
             };
        }



        [HttpPost]
        public async Task<IActionResult> PostComplain([FromBody] Complain complain)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Complain.Add(complain);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ComplainExists(complain.IdComplain))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetComplain", new { id = complain.IdComplain }, complain);
        }

        // DELETE: api/Complains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplain([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var complain = await _context.Complain.SingleOrDefaultAsync(m => m.IdComplain == id);
            if (complain == null)
            {
                return NotFound();
            }

            _context.Complain.Remove(complain);
            await _context.SaveChangesAsync();

            return Ok(complain);
        }

        private bool ComplainExists(string id)
        {
            return _context.Complain.Any(e => e.IdComplain == id);
        }
    }
}