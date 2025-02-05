using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_RestApi.GameData;
using Project_RestApi.Models;

namespace Project_RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly GameDataContext _context;

        public PlayerController(GameDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Player>> GetAll()
        {
            return await _context.Players.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var player = await _context.Players.FindAsync(id);

            if (player == null)
                return NotFound();

            return Ok(player);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Player player)
        {
            if (player == null)
                return BadRequest("Invalid player data");

            var newPlayer = new Player
            {
                Name = player.Name,
                Rank = player.Rank,
                Score = player.Score,
                EMail = player.EMail
            };

            _context.Players.Add(newPlayer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newPlayer.id }, newPlayer);
        }
    }
}
