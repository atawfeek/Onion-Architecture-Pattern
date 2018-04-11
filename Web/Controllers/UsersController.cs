using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MLS.Domain;
using MLS.Infrastructure.Data;
using MLS.Services.Interfaces;

namespace MLS.Web.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IUserService _iUserService;
        private readonly ILoggerFactory _iLogger;

        public UsersController(IUserService iUserService, ILoggerFactory iLogger)
        {
            _iUserService = iUserService;
            _iLogger = iLogger;
        }

        // GET: api/Users
        [EnableCors("ebookPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var _users = await _iUserService.GetUsers();
                return Ok(_users);
            }
            catch(Exception ex)
            {
                _iLogger.CreateLogger<MLS.Domain.User>().LogError(ex.Message);
                return StatusCode(500);
            }
        }

        /*
// GET: api/Users/5
[HttpGet("{id}")]
public async Task<IActionResult> GetUser([FromRoute] string id)
{
   if (!ModelState.IsValid)
   {
       return BadRequest(ModelState);
   }

   var mLSUser = await _context.AppUsers.SingleOrDefaultAsync(m => m.Id == id);

   if (mLSUser == null)
   {
       return NotFound();
   }

   return Ok(mLSUser);
}

// PUT: api/Users/5
[HttpPut("{id}")]
public async Task<IActionResult> PutMLSUser([FromRoute] string id, [FromBody] MlsUser mLSUser)
{
   if (!ModelState.IsValid)
   {
       return BadRequest(ModelState);
   }

   if (id != mLSUser.Id)
   {
       return BadRequest();
   }

   _context.Entry(mLSUser).State = EntityState.Modified;

   try
   {
       await _context.SaveChangesAsync();
   }
   catch (DbUpdateConcurrencyException)
   {
       if (!MLSUserExists(id))
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

// POST: api/Users
[HttpPost]
public async Task<IActionResult> PostMLSUser([FromBody] MlsUser mLSUser)
{
   if (!ModelState.IsValid)
   {
       return BadRequest(ModelState);
   }

   _context.AppUsers.Add(mLSUser);
   await _context.SaveChangesAsync();

   return CreatedAtAction("GetMLSUser", new { id = mLSUser.Id }, mLSUser);
}

// DELETE: api/Users/5
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteMLSUser([FromRoute] string id)
{
   if (!ModelState.IsValid)
   {
       return BadRequest(ModelState);
   }

   var mLSUser = await _context.AppUsers.SingleOrDefaultAsync(m => m.Id == id);
   if (mLSUser == null)
   {
       return NotFound();
   }

   _context.AppUsers.Remove(mLSUser);
   await _context.SaveChangesAsync();

   return Ok(mLSUser);
}

private bool MLSUserExists(string id)
{
   return _context.AppUsers.Any(e => e.Id == id);
}
*/
    }
}