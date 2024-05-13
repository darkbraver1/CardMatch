using ZooplaMatch.Domain.Models;

namespace ZooplaMatch.Application.Managers
{
    public class DeckManager : IDeckManger
    {
        private readonly Deck _deck;
        private readonly GameRuleManger _gameRuleManger;
        private readonly GraveYardManager _graveYardManager;

        public DeckManager(
            Deck deck, 
            GameRuleManger gameRuleManger, 
            GraveYardManager graveYardManager)
        {
            _deck = deck;
            _gameRuleManger = gameRuleManger;
            _graveYardManager = graveYardManager;
        }

        public void ShuffleCardsIntheDeck()
        {
            var random = new Random();
            int totalCards = _deck.Cards.Count();

            while (totalCards > 1)
            {
                totalCards--;
                var nextCard = random.Next(totalCards + 1);
                var temp = _deck.Cards[totalCards];
                _deck.Cards[totalCards] = _deck.Cards[nextCard];
                _deck.Cards[nextCard] = temp;
            }

            Console.WriteLine("Cards have been shuffled");
        }

        public List<Card> GetMatchingCardsFromDeck()
        {
            var matchedCards = new List<Card>();
            for (int i = 0; i < _deck.Cards.Count - 1; i++)
            {
                if(_gameRuleManger.IsAMatch((_deck.Cards[i], _deck.Cards[i + 1])))
                {                   
                    matchedCards.Add(_deck.Cards[i]);
                    matchedCards.Add(_deck.Cards[i + 1]);
                    _deck.Cards.RemoveAt(i);
                    _deck.Cards.RemoveAt(i + 1);
                    break;
                }
            }
            return matchedCards;
        }

        public Card DrawCardFromDeck()
        {
            var card = _deck.Cards[0];
            _deck.Cards.RemoveAt(0);
            return card;
        }

        public void SendNonDrawableCardsToGraveYard()
        {
            if(_deck.Cards.Count == 2)
            {
                _graveYardManager.AddCardsToGraveYard(_deck.Cards);

                Console.WriteLine($"{_deck.Cards.Count} non drawable cards were sent to graveyard");
            }
        }

        public bool IsDeckDrawable() => _deck.Cards.Count >= 4;
    }
}
