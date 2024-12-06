using Microsoft.AspNetCore.JsonPatch;
using ServicesContracts;
using TournamentCore.DTOs;

namespace TournamentServices
{
    public class GameService : IGameService
    {
        public async Task<IEnumerable<GameDTO>> GetGamesAsync(bool trackChanges = false)
        {
            throw new NotImplementedException();
        }

        public async Task<GameDTO> GetGameAsync(string identifier, bool trackChanges = false)
        {
            throw new NotImplementedException();
        }

        public async Task PostGame(GameCreationDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task PutTournament(int id, GameDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteGame(int id)
        {
            throw new NotImplementedException();
        }

        public async Task PatchGame(int id, JsonPatchDocument<GameDTO> patchDocument)
        {
            throw new NotImplementedException();
        }
    }
}
