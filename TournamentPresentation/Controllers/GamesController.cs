using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts;
using TournamentCore.DTOs;
using TournamentCore.Entities;

namespace TournamentPresentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public GamesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGame()
        {
            var result = await _serviceManager.GameService.GetGamesAsync();
            return Ok(result.Data);
        }

        // GET: api/Games/5
        [HttpGet("{identifier}")]
        public async Task<ActionResult<Game>> GetGame(string identifier)
        {
            var result = await _serviceManager.GameService.GetGameAsync(identifier);
            if (!result.Success) return NotFound(result.ErrorMessage);
            return Ok(result.Data);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, GameDTO dto)
        {
            var result = await _serviceManager.GameService.PutGame(id, dto);
            if (!result.Success) return NotFound(result.ErrorMessage);
            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(GameCreationDTO dto)
        {
            var result = await _serviceManager.GameService.PostGame(dto);
            if (!result.Success)
            {
                // 10 games per tournament constraint. If >9 ErrorMessage starts with "N"
                if (result.ErrorMessage.StartsWith("N")) return BadRequest(result.ErrorMessage);
                return NotFound(result.ErrorMessage);
            }
            return CreatedAtAction(nameof(GetGame), new { id = result.Data.Id }, result.Data);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var result = await _serviceManager.GameService.DeleteGame(id);
            if (!result.Success) return NotFound(result.ErrorMessage);
            return NoContent();
        }

        // PATCH: api/Games/5
        [HttpPatch("{gameId}")]
        public async Task<ActionResult<GameDTO>> PatchGame(int gameId, [FromBody]
            JsonPatchDocument<GameDTO> patchDocument)
        {
            var result = await _serviceManager.GameService.PatchGame(gameId, patchDocument);
            if (!result.Success) return BadRequest(result.ErrorMessage);
            return NoContent();
        }

    }
}
