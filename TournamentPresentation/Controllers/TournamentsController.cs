using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TournamentCore.DTOs;
using ServicesContracts;

namespace TournamentPresentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public TournamentsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDTO>>> GetTournament(bool includeGames, int pageSize = 20, int pageNumber = 1)
        {
            var result = await _serviceManager.TournamentService.GetTournamentsAsync(includeGames, pageSize, pageNumber);
            return Ok(result.Data);
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDTO>> GetTournament(int id)
        {
            var result = await _serviceManager.TournamentService.GetTournamentAsync(id);
            if (!result.Success) return NotFound(result.ErrorMessage);
            return Ok(result.Data);
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, TournamentDTO dto)
        {
            var result = await _serviceManager.TournamentService.PutTournament(id, dto);
            if (!result.Success) return NotFound(result.ErrorMessage);
            return NoContent();
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TournamentDTO>> PostTournament(TournamentCreationDTO dto)
        {
            var result = await _serviceManager.TournamentService.PostTournament(dto);
            return CreatedAtAction(nameof(GetTournament), new { id = result.Data.Id }, result.Data); //DOES THIS WORK LOL?
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var result = await _serviceManager.TournamentService.DeleteTournament(id);
            if (!result.Success) return NotFound(result.ErrorMessage);
            return NoContent();
        }

        // PATCH: api/Tournaments/5
        [HttpPatch("{tournamentId}")]
        public async Task<ActionResult<TournamentDTO>> PatchTournament(int id, [FromBody]
            JsonPatchDocument<TournamentDTO> patchDocument)
        {
            var result = await _serviceManager.TournamentService.PatchTournament(id, patchDocument);
            if (!result.Success) return NotFound("Not found or validation error.");
            return NoContent();
        }
    }
}
