using ServicesContracts;
using TournamentCore.DTOs;
using TournamentCore.Entities;

namespace TournamentServices.Mapping
{
    public class Mapper : IMapper
    {
        public Game MapToCreationGame(GameCreationDTO dto)
        {
            return GameMapper.CreationToGame(dto);
        }

        public Tournament MapToCreationTournament(TournamentCreationDTO dto)
        {
            return TournamentMapper.CreationToTournament(dto);
        }

        public GameDTO MapToGameDTO(Game game)
        {
            return GameMapper.ToDTO(game);
        }

        public TournamentDTO MapToTournamentDTO(Tournament tournament)
        {
            return TournamentMapper.ToDTO(tournament);
        }

        public Game MapToUpdateGame(GameDTO dto)
        {
            throw new NotImplementedException();
        }

        public Tournament MapToUpdateTournament(TournamentDTO dto)
        {
            return TournamentMapper.UpdateToTournament(dto);
        }
    }
}
