using ZooplaMatch.Domain.Models;
using ZooplaMatch.Domain.Models.Enums;

namespace ZooplaMatch.Application.Managers
{
    public class GameRuleManger
    {
        private readonly CardMatchType _matchType;

        public GameRuleManger(CardMatchType matchType)
        {
            _matchType = matchType;
        }

        public bool IsAMatch((Card, Card) cards)
        {
            return _matchType switch
            {
                CardMatchType.Suit => cards.Item1.Suit == cards.Item2.Suit,
                CardMatchType.Rank => cards.Item1.Rank == cards.Item2.Rank,
                CardMatchType.SuitAndRank => cards.Item1.Suit == cards.Item2.Suit && cards.Item1.Rank == cards.Item2.Rank,
                _ => throw new NotImplementedException("Selected Matching Type is not implemented")
            };
        }
    }
}
