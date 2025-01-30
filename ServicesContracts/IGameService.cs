using Microsoft.AspNetCore.JsonPatch;
using TournamentCore.DTOs;
using TournamentShared;

namespace ServicesContracts
{
    public interface IGameService
    {
        Task<ServiceResult<IEnumerable<GameDTO>>> GetGamesAsync(int pageSize, int pageNumber);
        Task<ServiceResult<GameDTO>> GetGameAsync(string identifier);
        Task<ServiceResult<GameDTO>> PostGame(GameCreationDTO dto); 
        Task<ServiceResult<GameDTO>> PutGame(int id, GameDTO dto);
        Task<ServiceResult<GameDTO>> DeleteGame(int id);
        Task<ServiceResult<GameDTO>> PatchGame(int id, JsonPatchDocument<GameDTO> patchDocument);
    }
}
