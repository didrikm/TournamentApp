using Microsoft.EntityFrameworkCore;
using TournamentCore.Entities;
using TournamentCore.Repositories;
using TournamentData.Data;
namespace TournamentData.Repositories
{
    public class GameRepo : IGameRepo
    {
        private readonly TournamentApiContext _context;

        public GameRepo(TournamentApiContext context)
        {
            _context = context;
        }
        public void Add(Game Game)
        {
            _context.Game.Add(Game);
        }

        public async Task<bool> AnyGameAsync(int id)
        {
            return await _context.Game.AnyAsync(g => g.Id == id);
        }

        public async Task<Game?> GetGameAsync(int id)
        {
            return await _context.Game.FindAsync(id);
        }

        public async Task<Game?> GetGameAsync(string title)
        {
            return await _context.Game.FirstOrDefaultAsync(g => g.Title == title);
        }
        public async Task<IEnumerable<Game>> GetGamesAsync()
        {
            return await _context.Game.ToListAsync();
        }

        public void Remove(Game Game)
        {
            _context?.Game.Remove(Game);
        }

        public void Update(Game Game)
        {
            _context.Update(Game);
        }
    }
}
