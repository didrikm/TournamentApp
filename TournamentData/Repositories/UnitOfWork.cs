using TournamentCore.Repositories;
using TournamentData.Data;

namespace TournamentData.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TournamentApiContext _context;

        public UnitOfWork(TournamentApiContext context)
        {
            _context = context;
            TournamentRepo = new TournamentRepo(context);
            GameRepo = new GameRepo(context);
        }
        public ITournamentRepo TournamentRepo { get; }

        public IGameRepo GameRepo {  get; }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
