namespace TournamentCore.DTOs
{
    public record GameCreationDTO
    {
         public string Title { get; init; }
        public DateTime Time { get; init; }
        public int TournamentId { get; init; }
        public TournamentDTO tournament { get; init; }

    }
}
