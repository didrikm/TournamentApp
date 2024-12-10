using Microsoft.AspNetCore.JsonPatch;
using TournamentCore.DTOs;
using TournamentShared;

namespace ServicesContracts
{
    public interface ITournamentService
    {
        Task<ServiceResult<IEnumerable<TournamentDTO>>> GetTournamentsAsync(bool includeGames,  int pageSize = 20, int page = 1);
        Task<ServiceResult<TournamentDTO>> GetTournamentAsync(int id);
        Task<ServiceResult<TournamentDTO>> PostTournament(TournamentCreationDTO dto);  
        Task<ServiceResult<TournamentDTO>> PutTournament(int id, TournamentDTO dto); //TODO: fulfix, NoContent så borde inte vara TournamentDTO men void är inte accepterat
        Task<ServiceResult<TournamentDTO>> DeleteTournament(int id);
        Task<ServiceResult<TournamentDTO>> PatchTournament(int id, JsonPatchDocument<TournamentDTO> patchDocument);
    }
}
