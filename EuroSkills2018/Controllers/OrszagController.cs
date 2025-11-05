using EuroSkills2018.Data;
using EuroSkills2018.DTOs;
using EuroSkills2018.Models;
using Microsoft.AspNetCore.Http;
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
    }
}
