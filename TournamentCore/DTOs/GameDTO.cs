namespace TournamentCore.DTOs
{
    public record GameDTO
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public DateTime Time { get; init; }
        public int TournamentId { get; init; }
    }
}
