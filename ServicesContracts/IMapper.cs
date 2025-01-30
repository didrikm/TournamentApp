using TournamentCore.DTOs;
using TournamentCore.Entities;

namespace ServicesContracts
{
    public interface IMapper
    {
        TournamentDTO MapToTournamentDTO(Tournament tournament);
        Tournament MapToCreationTournament(TournamentCreationDTO dto);
        Tournament MapToUpdateTournament(TournamentDTO dTO);
        GameDTO MapToGameDTO(Game game);
        Game MapToCreationGame(GameCreationDTO dto);
        Game MapToUpdateGame(GameDTO dto);
    }
}