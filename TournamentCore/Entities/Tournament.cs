namespace TournamentCore.Entities
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
