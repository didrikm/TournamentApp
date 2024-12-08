using TournamentCore.DTOs;
using TournamentCore.Entities;

namespace TournamentServices.Mapping
{
    public static class TournamentMapper
    {
        public static TournamentDTO ToDTO(Tournament tournament)
        {
            return new TournamentDTO
            {
                Id = tournament.Id,
                Title = tournament.Title,
                StartDate = tournament.StartDate,
                Games = tournament.Games?.Select(g => new GameDTO
                {
                    Id = g.Id,
                    Title = g.Title,
                    Time = g.Time
                }).ToList()
                ?? new List<GameDTO>()
            };
        }
        public static Tournament CreationToTournament(TournamentCreationDTO dto)
        {
            return new Tournament
            {
                Title = dto.Title,
                StartDate = dto.StartDate,
                Games = new List<Game>()
            };
        }
        public static Tournament UpdateToTournament(TournamentDTO dto)
        {
            return new Tournament
            {
                Id = dto.Id,
                Title = dto.Title,
                StartDate = dto.StartDate,
                Games = new List<Game>()
            };
        }
    }
}
