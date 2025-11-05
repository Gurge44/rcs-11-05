using EuroSkills2018.Data;
using EuroSkills2018.DTOs;
using EuroSkills2018.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EuroSkills2018.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersenyzoController : ControllerBase
    {
        private readonly ESDbContext _context;
        public VersenyzoController(ESDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Versenyzo>>> GetVersenyzok()
        {
            return await _context.Versenyzok.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Versenyzo>> GetVersenyzo(int id)
        {
            var versenyzo = await _context.Versenyzok.FindAsync(id);
            if (versenyzo == null) return NotFound();
            return Ok(versenyzo);
        }

        [HttpPost]
        public async Task<ActionResult> AddVersenyzo(VersenyzoDTO versenyzo)
        {
            Versenyzo ujVersenyzo = new()
            {
                Nev = versenyzo.Nev,
                SzakmaId = versenyzo.SzakmaId,
                OrszagId = versenyzo.OrszagId,
                Pont = versenyzo.Pont
            };
            _context.Versenyzok.Add(ujVersenyzo);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchVersenyzo(int id, [FromBody] JsonPatchDocument<Versenyzo> patcher)
        {
            if (patcher == null) return BadRequest();

            var versenyzo = await _context.Versenyzok.FindAsync(id);
            if (versenyzo == null) return NotFound();

            patcher.ApplyTo(versenyzo);

            return Ok(versenyzo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVersenyzo(int id)
        {
            var versenyzo = await _context.Versenyzok.FindAsync(id);
            if (versenyzo == null) return NotFound();

            _context.Versenyzok.Remove(versenyzo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
