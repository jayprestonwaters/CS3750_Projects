namespace Hangman.Models
{
    public class Scores
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int Score { get; set; }

        public string? WordPlayed { get; set; }
    }
}
