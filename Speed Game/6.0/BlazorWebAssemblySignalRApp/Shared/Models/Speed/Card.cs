
namespace BlazorWebAssemblySignalRApp.Shared.Models.Speed
{
    public class Card
    {
        public enum CardValue
        {
            Ace = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Jack = 11,
            Queen = 12,
            King = 13
        }
    
        public enum CardSuit
        {
            Hearts,
            Clubs,
            Diamonds,
            Spades
        }
        public CardSuit Suit { get; set; }
        public CardValue Value { get; set; }
        public int ImageName { get; set; }
        public bool IsVisible { get; set; }

        public Card()
        {

        }

        public Card(int cardValue, CardSuit suit)
        {
            Value = (CardValue)cardValue;
            Suit = suit;

            int x;

            if (suit == CardSuit.Spades)
            {
                x = cardValue;
            }
            else if (suit == CardSuit.Hearts)
            {
                x = cardValue + 13;
            }
            else if (suit == CardSuit.Diamonds)
            {
                x = cardValue + 26;
            }
            else
            {
                x = cardValue + 39;
            }

            ImageName = x;
        }
    
    }
}
