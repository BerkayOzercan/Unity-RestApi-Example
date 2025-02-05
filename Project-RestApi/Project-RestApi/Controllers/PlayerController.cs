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
    }
}
