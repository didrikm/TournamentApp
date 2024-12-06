using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentCore.DTOs;
using TournamentCore.Entities;
using TournamentCore.Mapping;
using TournamentCore.Repositories;

namespace TournamentPresentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GamesController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGame()
        {
            var games = await _uow.GameRepo.GetGamesAsync();
            var gameDTOs = games.Select(_mapper.MapToGameDTO).ToList();
            return Ok(gameDTOs);
        }

        // GET: api/Games/5
        [HttpGet("{identifier}")]
        public async Task<ActionResult<Game>> GetGame(string identifier)
        {
            Game? game;
            if (int.TryParse(identifier, out int id))
            {
                game = await _uow.GameRepo.GetGameAsync(id);
            }
            else
            {
                game = await _uow.GameRepo.GetGameAsync(identifier);
            }
            if (game == null)
            {
                return NotFound();
            }

            var gameDTO = _mapper.MapToGameDTO(game);

            return Ok(gameDTO);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, GameDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var oldGame = await _uow.GameRepo.GetGameAsync(id);

            if (oldGame == null)
            {
                return NotFound("The specified GameId does not exist.");
            }

            if (!await _uow.TournamentRepo.AnyTournamentAsync(dto.TournamentId))
            {
                return BadRequest("The specified TournamentId does not exist.");
            }

            oldGame.Title = dto.Title;
            oldGame.Time = dto.Time;
            oldGame.TournamentId = dto.TournamentId;

            _uow.GameRepo.Update(oldGame);

            try
            {
                await _uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.GameRepo.AnyGameAsync(id))
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(GameCreationDTO dto)
        {
            var game = _mapper.MapToCreationGame(dto);
            var tournament = await _uow.TournamentRepo.GetTournamentAsync(dto.TournamentId);
            game.tournament = tournament;
            _uow.GameRepo.Add(game);
            await _uow.CompleteAsync();

            return CreatedAtAction(nameof(GetGame), new { id = game.Id }, _mapper.MapToGameDTO(game));

        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _uow.GameRepo.GetGameAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _uow.GameRepo.Remove(game);
            await _uow.CompleteAsync();

            return NoContent();
        }

        // PATCH: api/Games/5
        [HttpPatch("{gameId}")]
        public async Task<ActionResult<GameDTO>> PatchGame(int gameId, [FromBody]
            JsonPatchDocument<GameDTO> patchDocument)
        {

            if (patchDocument == null)
            {
                return BadRequest();
            }

            var oldGame = await _uow.GameRepo.GetGameAsync(gameId);

            if (oldGame == null)
            {
                return NotFound();
            }

            var gameDTO = _mapper.MapToGameDTO(oldGame);

            patchDocument.ApplyTo(gameDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            oldGame.Title = gameDTO.Title;
            oldGame.Time = gameDTO.Time;

            _uow.GameRepo.Update(oldGame);

            try
            {
                await _uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.GameRepo.AnyGameAsync(gameId))
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

    }
}
