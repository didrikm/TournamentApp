using TournamentCore.Entities;

namespace TournamentCore.Repositories
{
    public interface ITournamentRepo
    {
        Task<IEnumerable<Tournament>> GetTournamentsAsync(bool includeGames, int pageSize, int pageNumber);
        Task<Tournament?> GetTournamentAsync(int id);
        Task<bool> AnyTournamentAsync(int id);
        void Add(Tournament tournament);
        void Remove(Tournament tournament);
        void Update(Tournament tournament);
    }
}
