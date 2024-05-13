using ZooplaMatch.Domain.Models;

namespace ZooplaMatch.Application.Managers
{
    public class GraveYardManager
    {
        private readonly GraveYard _graveYard;

        public GraveYardManager(GraveYard graveYard)
        {
            _graveYard = graveYard;
        }

        public void AddCardsToGraveYard(List<Card> cards) => _graveYard.Cards.AddRange(cards);

        public string CardsIntheGraveYard()
        {
            return $"{_graveYard.Cards.Count} cards are in the graveyard";
        }
    }
}
