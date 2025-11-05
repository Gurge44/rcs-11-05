using EuroSkills2018.Data;
using EuroSkills2018.DTOs;
using EuroSkills2018.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EuroSkills2018.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrszagController : ControllerBase
    {
        private readonly ESDbContext _context;
        public OrszagController(ESDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Orszag>>> GetOrszagok()
        {
            return await _context.Orszagok.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Orszag>> GetOrszag(int id)
        {
            var orszag = await _context.Orszagok.FindAsync(id);
            if (orszag == null) return NotFound();
            return Ok(orszag);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrszag(string orszagNev)
        {
            Orszag ujOrszag = new()
            {
                Nev = orszagNev
            };
            _context.Orszagok.Add(ujOrszag);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchOrszag(string id, [FromBody] JsonPatchDocument<Orszag> patcher)
        {
            if (patcher == null) return BadRequest();

            var orszag = await _context.Orszagok.FindAsync(id);
            if (orszag == null) return NotFound();

            patcher.ApplyTo(orszag);

            return Ok(orszag);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrszag(string id)
        {
            var orszag = await _context.Orszagok.FindAsync(id);
            if (orszag == null) return NotFound();

            _context.Orszagok.Remove(orszag);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
