using Microsoft.AspNetCore.JsonPatch;
using TournamentCore.DTOs;

namespace ServicesContracts
{
    public interface IGameService
    {
        Task<IEnumerable<GameDTO>> GetGamesAsync(bool trackChanges = false);
        Task<GameDTO> GetGameAsync(string identifier, bool trackChanges = false);
        Task PostGame(GameCreationDTO dto);  // TODO: custom result object?
        Task PutTournament(int id, GameDTO dto);
        Task DeleteGame(int id);
        Task PatchGame(int id, JsonPatchDocument<GameDTO> patchDocument);
    }
}
