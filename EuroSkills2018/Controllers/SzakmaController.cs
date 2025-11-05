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
    public class SzakmaController : ControllerBase
    {
        private readonly ESDbContext _context;
        public SzakmaController(ESDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Szakma>>> GetSzakmak()
        {
            return await _context.Szakmak.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Szakma>> GetSzakma(int id)
        {
            var szakma = await _context.Szakmak.FindAsync(id);
            if (szakma == null) return NotFound();
            return Ok(szakma);
        }

        [HttpPost]
        public async Task<ActionResult> AddSzakma(string szakmaNev)
        {
            Szakma ujSzakma = new()
            {
                Nev = szakmaNev
            };
            _context.Szakmak.Add(ujSzakma);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchSzakma(string id, [FromBody] JsonPatchDocument<Szakma> patcher)
        {
            if (patcher == null) return BadRequest();

            var szakma = await _context.Szakmak.FindAsync(id);
            if (szakma == null) return NotFound();

            patcher.ApplyTo(szakma);

            return Ok(szakma);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSzakma(string id)
        {
            var szakma = await _context.Szakmak.FindAsync(id);
            if (szakma == null) return NotFound();

            _context.Szakmak.Remove(szakma);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
