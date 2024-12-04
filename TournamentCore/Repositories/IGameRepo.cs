using TournamentCore.Entities;

namespace TournamentCore.Repositories
{
    public interface IGameRepo
    {
        Task<IEnumerable<Game>> GetGamesAsync();
        Task<Game?> GetGameAsync(int id);
        Task<Game?> GetGameAsync(string title);
        Task<bool> AnyGameAsync(int id);
        void Add(Game Game);
        void Remove(Game Game);
        void Update(Game Game);
    }
}
