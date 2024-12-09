using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using ServicesContracts;
using TournamentCore.DTOs;
using TournamentCore.Entities;
using TournamentCore.Repositories;
using TournamentShared;

namespace TournamentServices
{
    public class GameService : IGameService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public GameService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<ServiceResult<IEnumerable<GameDTO>>> GetGamesAsync(bool trackChanges = false)
        {
            var games = await _uow.GameRepo.GetGamesAsync();
            var dtos = games.Select(_mapper.MapToGameDTO).ToList();
            return ServiceResult<IEnumerable<GameDTO>>.Ok(dtos);
        }

        public async Task<ServiceResult<GameDTO>> GetGameAsync(string identifier, bool trackChanges = false)
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
                return ServiceResult<GameDTO>.NotFound("Game was not found.");
            }

            var gameDTO = _mapper.MapToGameDTO(game);
            return ServiceResult<GameDTO>.Ok(gameDTO);
        }

        public async Task<ServiceResult<GameDTO>> PostGame(GameCreationDTO dto)
        {
            var game = _mapper.MapToCreationGame(dto);
            var tournament = await _uow.TournamentRepo.GetTournamentAsync(dto.TournamentId);
            if (tournament == null) return ServiceResult<GameDTO>.BadRequest("Associated tournament not found.");
            if (tournament.Games.Count > 9) return ServiceResult<GameDTO>.BadRequest("No more than 10 games per tournament.");
            game.tournament = tournament;
            _uow.GameRepo.Add(game);
            await _uow.CompleteAsync();
            var returnDTO = _mapper.MapToGameDTO(game);
            return ServiceResult<GameDTO>.Ok(returnDTO);
        }

        public async Task<ServiceResult<GameDTO>> PutGame(int id, GameDTO dto)
        {
            if (id != dto.Id)
            {
                return ServiceResult<GameDTO>.BadRequest("Route and body do not match.");
            }

            var oldGame = await _uow.GameRepo.GetGameAsync(id);

            if (oldGame == null)
            {
                return ServiceResult<GameDTO>.NotFound("The specified GameId does not exist.");
            }

            if (!await _uow.TournamentRepo.AnyTournamentAsync(dto.TournamentId))
            {
                return ServiceResult<GameDTO>.NotFound("The specified GameId does not exist.");
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
                    return ServiceResult<GameDTO>.NotFound("The specified GameId does not exist.");
                }
            }
            return ServiceResult<GameDTO>.NoContent();
        }

        public async Task<ServiceResult<GameDTO>> DeleteGame(int id)
        {
            var game = await _uow.GameRepo.GetGameAsync(id);
            if (game == null)
            {
                return ServiceResult<GameDTO>.NotFound("Game not found.");
            }
            _uow.GameRepo.Remove(game);
            await _uow.CompleteAsync();
            return ServiceResult<GameDTO>.NoContent();
        }

        public async Task<ServiceResult<GameDTO>> PatchGame(int id, JsonPatchDocument<GameDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return ServiceResult<GameDTO>.BadRequest("Missing patch document.");
            }

            var oldGame = await _uow.GameRepo.GetGameAsync(id);

            if (oldGame == null)
            {
                return ServiceResult<GameDTO>.NotFound("Id not found.");
            }

            var gameDTO = _mapper.MapToGameDTO(oldGame);

            patchDocument.ApplyTo(gameDTO);

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //TODO: implementera validering

            oldGame.Title = gameDTO.Title;
            oldGame.Time = gameDTO.Time;

            _uow.GameRepo.Update(oldGame);

            try
            {
                await _uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.GameRepo.AnyGameAsync(id))
                {
                    return ServiceResult<GameDTO>.NotFound("Id not found in DB.");
                }
            }
            return ServiceResult<GameDTO>.NoContent();
        }
    }
}
