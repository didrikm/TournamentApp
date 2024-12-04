using TournamentCore.DTOs;
using TournamentCore.Entities;

namespace TournamentCore.Mapping
{
    public static class GameMapper
    {
        public static GameDTO ToDTO(Game game)
        {
            return new GameDTO
            {
                Id = game.Id,
                Title = game.Title,
                Time = game.Time,
                TournamentId = game.TournamentId,
            };
        }
        public static Game CreationToGame(GameCreationDTO dto)
        {
            return new Game
            {
                Title = dto.Title,
                Time = dto.Time,
                TournamentId = dto.TournamentId,
                tournament = null // TODO: set in service layer
            };
        }
        public static Game UpdateToGame(GameDTO dto)
        {
            return new Game
            {
                Id = dto.Id,
                Title = dto.Title,
                Time = dto.Time,
                TournamentId = dto.TournamentId,
                tournament = null // TODO: set in service layer
            };
        }
    }
}
