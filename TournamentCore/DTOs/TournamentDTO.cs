namespace TournamentCore.DTOs
{
    public record TournamentDTO
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public DateTime StartDate { get; init; }
        public ICollection<GameDTO>? Games { get; init; }
    }
}
