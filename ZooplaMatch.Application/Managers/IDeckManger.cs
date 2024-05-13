using ZooplaMatch.Domain.Models;

namespace ZooplaMatch.Application.Managers
{
    public interface IDeckManger
    {
        void ShuffleCardsIntheDeck();

        Card DrawCardFromDeck();

        bool IsDeckDrawable();

        void SendNonDrawableCardsToGraveYard();

        List<Card> GetMatchingCardsFromDeck();
    }
}
