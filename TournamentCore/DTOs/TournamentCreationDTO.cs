namespace TournamentCore.DTOs
{
    public record TournamentCreationDTO
    {
        public string Title { get; init; }
        public DateTime StartDate { get; init; }
        //public ICollection<GameDTO> Games {get; init;}

    }
}
