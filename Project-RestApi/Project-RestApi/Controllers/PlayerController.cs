using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_RestApi.GameData;
using Project_RestApi.Interfaces;
using Project_RestApi.Models;
using Project_RestApi.Services;

namespace Project_RestApi.Controllers
{
    [Authorize]
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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(string userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var player = await _context.Players.FirstOrDefaultAsync(p => p.UserId == userId); ;

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
                UserName = player.UserName,
                Rank = player.Rank,
                Score = player.Score,
                UserId = player.UserId
            };

            _context.Players.Add(newPlayer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { userId = newPlayer.UserId }, newPlayer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Player updatedPlayer)
        {
            if (id != updatedPlayer.Id) return BadRequest();

            var player = await _context.Players.FindAsync(id);

            if (player == null) return NotFound();

            player.UserName = updatedPlayer.UserName;
            player.Rank = updatedPlayer.Rank;
            player.Score = updatedPlayer.Score;

            await _context.SaveChangesAsync();

            return Ok(player);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var player = await _context.Players.FirstOrDefaultAsync(x => x.Id == id);

            if (player == null)
                return NotFound();

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
