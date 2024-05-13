using ZooplaMatch.Domain.Models;
using ZooplaMatch.Models.Enums;

namespace ZooplaMatch.Application.Managers
{
    public class GameManager : IGameManager
    {
        private readonly IDeckManger _deckManger;
        private readonly GameRuleManger _gameRuleManger;
        private readonly GraveYardManager _graveYardManager;
        private readonly Player _player1;
        private readonly Player _player2;

        public GameManager(
            IDeckManger deckManger, 
            GameRuleManger gameRuleManger,
            GraveYardManager graveYardManager,
            Player player1,
            Player player2)
        {
            _deckManger = deckManger;
            _gameRuleManger = gameRuleManger;
            _graveYardManager = graveYardManager;
            _player1 = player1;
            _player2 = player2;
        }

        public async Task StartGame()
        {
            _deckManger.ShuffleCardsIntheDeck();

            ProcessLuckyDrawFirstMatchWinner();

            await DealCardsToPlayers();            
        }

        public Task<string> EndGame()
        {
            if(_player1.MatchedCards.Count == _player2.MatchedCards.Count)
            {
                return Task.FromResult($"The match is a draw with {_player1.MatchedCards.Count / 2} points each");
            }
            else if(_player1.MatchedCards.Count > _player2.MatchedCards.Count)
            {
                return Task.FromResult($"{PlayerName.CPU1} has won by {_player1.MatchedCards.Count / 2} points");
            }
            else
            {
                return Task.FromResult($"{PlayerName.CPU2} has won by {_player2.MatchedCards.Count / 2} points");
            }
        }

        private async Task DealCardsToPlayers()
        {
            while (_deckManger.IsDeckDrawable())
            {
                for (int i = 0; i < 2; i++)
                {
                   _player1.OnHandCards.Add(_deckManger.DrawCardFromDeck());

                   _player2.OnHandCards.Add(_deckManger.DrawCardFromDeck());
                }

                if (_player1.OnHandCards.Count == 2 && _player2.OnHandCards.Count == 2)
                {
                    var player1Task = AddToMatchedCardsOrSendToGraveYard(_player1);
                    var player2Task = AddToMatchedCardsOrSendToGraveYard(_player2);

                    await Task.WhenAll(player1Task, player2Task);
                }
            }

            _deckManger.SendNonDrawableCardsToGraveYard();
        }

        private Task AddToMatchedCardsOrSendToGraveYard(Player player)
        {
            if (_gameRuleManger.IsAMatch((player.OnHandCards[0], player.OnHandCards[1])))
            {
                Console.WriteLine($"AddedToMatchedCollection Event: Player {player.PlayerName} got cards {GetCardsString(player.OnHandCards)}");
                player.MatchedCards.AddRange(player.OnHandCards);
                player.OnHandCards.Clear();
            }
            else
            {
                Console.WriteLine($"SentToGraveYard Event:Player {player.PlayerName} got cards {GetCardsString(player.OnHandCards)}");
                _graveYardManager.AddCardsToGraveYard(player.OnHandCards);
                player.OnHandCards.Clear();
            }

            return Task.CompletedTask;
        }

        private void ProcessLuckyDrawFirstMatchWinner()
        {
            Random random = new();

            var playerName = Enum.GetName(typeof(PlayerName), random.Next(Enum.GetValues(typeof(PlayerName)).Length));

            if (playerName == PlayerName.CPU1.ToString())
            {
                LuckDrawWinnerEffect(_player1);
            }
            else
            {
                LuckDrawWinnerEffect(_player2);
            }
        }

        private void LuckDrawWinnerEffect(Player player)
        {
            var drawnCards = _deckManger.GetMatchingCardsFromDeck();
            player.MatchedCards.AddRange(drawnCards);
            Console.WriteLine($"LuckyDrawEvent: Player {player.PlayerName} got a headstart with first match declaration with {GetCardsString(drawnCards)}");
        }

        private static string GetCardsString(List<Card> cards) => string.Join(", ", cards.Select(x => x.GetCard));
    }
}
