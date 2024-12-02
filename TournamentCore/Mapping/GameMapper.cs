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
                //tournament = game.tournament
            };
        }
    }
}
