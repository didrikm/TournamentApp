using Microsoft.AspNetCore.JsonPatch;
using TournamentCore.DTOs;
using TournamentShared;

namespace ServicesContracts
{
    public interface IGameService
    {
        Task<ServiceResult<IEnumerable<GameDTO>>> GetGamesAsync(bool trackChanges = false);
        Task<ServiceResult<GameDTO>> GetGameAsync(string identifier, bool trackChanges = false);
        Task<ServiceResult<GameDTO>> PostGame(GameCreationDTO dto);  // TODO: custom result object?
        Task<ServiceResult<GameDTO>> PutGame(int id, GameDTO dto);
        Task<ServiceResult<GameDTO>> DeleteGame(int id);
        Task<ServiceResult<GameDTO>> PatchGame(int id, JsonPatchDocument<GameDTO> patchDocument);
    }
}
