using ZooplaMatch.Domain.Models.Enums;

namespace ZooplaMatch.Domain.Models
{
    public class Deck
    {
        public List<Card> Cards { get; }

        public int Pack { get; }

        public Deck(int pack) 
        { 
            Cards = new List<Card>();
            CreateDeckFromPack(pack);
        }

        private void CreateDeckFromPack(int pack)
        {
            foreach(Rank rank in Enum.GetValues(typeof(Rank)))
            {
                foreach(Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    for (int i = 0; i < pack; i++)
                    {
                        Cards.Add(new Card(suit, rank));
                    }
                }
            }
        }
    }
}
