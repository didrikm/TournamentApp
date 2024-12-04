using Microsoft.AspNetCore.Mvc;
using TournamentCore.DTOs;
using TournamentCore.Entities;
using TournamentCore.Mapping;
using TournamentCore.Repositories;

namespace TournamentApi.Controllers
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
        [HttpGet("{id}")] 
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _uow.GameRepo.GetGameAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            var gameDTO = _mapper.MapToGameDTO(game);

            return Ok(gameDTO);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGame(int id, GameDTO gameDTO)
        //{
        //    if (id != gameDTO.Id)
        //    {
        //        return BadRequest();
        //    }
            
        //    if(!await _uow.GameRepo.AnyGameAsync(id))
        //    {
        //        return NotFound();
        //    }



        //    _context.Entry(game).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GameExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

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

        private async Task<bool> GameExistsAsync(int id)
        {
            return await _uow.GameRepo.AnyGameAsync(id);
        }
    }
}
