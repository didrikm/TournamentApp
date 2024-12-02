namespace TournamentCore.Repositories
{
    public interface IUnitOfWork
    {
        ITournamentRepo TournamentRepo {  get; }
        IGameRepo GameRepo { get; }
        Task CompleteAsync();
    }
}
