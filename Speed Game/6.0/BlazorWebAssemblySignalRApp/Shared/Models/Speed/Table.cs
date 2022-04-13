
namespace BlazorWebAssemblySignalRApp.Shared.Models.Speed
{
    public class Table
    {
        public List<Card> FullDeck { get; set; }
        public List<Card> PlayerOneHand { get; set; }
        public List<Card> PlayerTwoHand { get; set; }
        public Stack<Card> PlayerOneDraw { get; set; }
        public Stack<Card> PlayerTwoDraw { get; set; }
        public Stack<Card> PullOne { get; set; }
        public Stack<Card> PullTwo { get; set; }
        public Stack<Card> PlayOne { get; set; }
        public Stack<Card> PlayTwo { get; set; }
        public List<Card> Discard { get; set; }
        public string? PlayerOneGuid { get; set; }
        public string? PlayerTwoGuid { get; set; }

        public Card GetPlayOneTop => PlayOne.Peek();
        public Card GetPlayTwoTop => PlayTwo.Peek();

        public Table()
        {
            FullDeck = new List<Card>();
            PlayerOneHand = new List<Card>();
            PlayerTwoHand = new List<Card>();
            PlayerOneDraw = new Stack<Card>();
            PlayerTwoDraw = new Stack<Card>();
            PullOne = new Stack<Card>();
            PullTwo = new Stack<Card>();
            PlayOne = new Stack<Card>();
            PlayTwo = new Stack<Card>();
            Discard = new List<Card>();
        }

        public void GenerateFullDeck()
        {
            for (int i = 1; i < 53; i++)
            {
                if (i <= 13)
                {
                    FullDeck.Add(new(i, Card.CardSuit.Spades));
                }
                else if (i <= 26)
                {
                    FullDeck.Add(new(i - 13, Card.CardSuit.Hearts));
                }
                else if (i <= 39)
                {
                    FullDeck.Add(new(i - 26, Card.CardSuit.Diamonds));
                }
                else
                {
                    FullDeck.Add(new(i - 39, Card.CardSuit.Clubs));
                }
            }
        }

        public void Deal()
        {
            for (int i = 0; i < 5; i++)
            {
                PlayerOneHand.Add(FullDeck[i]);
            }
            for (int i = 5; i < 10; i++)
            {
                PlayerTwoHand.Add(FullDeck[i]);
            }
            for (int i = 10; i < 25; i++)
            {PlayerOneDraw.Push(FullDeck[i]);
            }
            for (int i = 25; i < 40; i++)
            {PlayerTwoDraw.Push(FullDeck[i]);
            }
            for (int i = 40; i < 45; i++)
            {
                PullOne.Push(FullDeck[i]);
            }
            for (int i = 45; i < 50; i++)
            {
                PullTwo.Push(FullDeck[i]);
            }
            PlayOne.Push(FullDeck[50]);
            PlayTwo.Push(FullDeck[51]);
            FullDeck.Clear();
        }

        private static readonly Random rng = new();
        public void Shuffle(List<Card> deck)
        {
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = deck[k];
                deck[k] = deck[n];
                deck[n] = value;
            }
        }

        public void NewGame()
        {
            GenerateFullDeck();
            Shuffle(FullDeck);
            Deal();
        }
    }
}
