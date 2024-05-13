using ZooplaMatch.Models.Enums;

namespace ZooplaMatch.Domain.Models
{
    public record Player(PlayerName PlayerName, List<Card> MatchedCards, List<Card> OnHandCards);
}
