using demo_hero_complete.Data;
using demo_hero_complete.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demo_hero_complete.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class HeroController : Controller
    {
        private readonly DataContext _context;
        public HeroController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHeros()
        {
            var heros = await _context.SuperHeroes.ToListAsync();
            
            if(heros == null)
            {
                return NotFound("No Heros Found");
            }
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHero(int id)
        {
            var heros = await _context.SuperHeroes.FindAsync(id);

            if (heros == null)
            {
                return NotFound("No Hero Found with the given id");
            }
            return Ok(heros);
        }
        [HttpPost]
        public async Task<IActionResult> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(Ok(await _context.SuperHeroes.ToListAsync()));
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHero(SuperHero hero)
        {
            var given_hero = await _context.SuperHeroes.FindAsync(hero.Id);

            if (hero == null)
            {
                return NotFound("No Hero Found with the given id");
            }
            given_hero.Description = hero.Description;
            given_hero.Name = hero.Name;    
            given_hero.Type = hero.Type;    
            given_hero.Planet = hero.Planet;
            await _context.SaveChangesAsync();

            return Ok(Ok(await _context.SuperHeroes.ToListAsync()));
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> deleteHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return NotFound("No hero found with given id");
            }
            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

    }
}
