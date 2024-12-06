using Microsoft.AspNetCore.JsonPatch;
using TournamentCore.DTOs;

namespace ServicesContracts
{
    public interface ITournamentService
    {
        Task<IEnumerable<TournamentDTO>> GetTournamentsAsync(bool includeGames, bool trackChanges = false);
        Task<TournamentDTO> GetTournamentAsync(int id, bool trackChanges = false);
        Task PostTournament(TournamentCreationDTO dto);  // TODO: custom result object?
        Task PutTournament(int id, TournamentDTO dto);
        Task DeleteTournament(int id);
        Task PatchTorunament(int id, JsonPatchDocument<TournamentDTO> patchDocument);
    }
}
