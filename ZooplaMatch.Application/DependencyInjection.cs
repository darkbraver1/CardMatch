using Microsoft.Extensions.DependencyInjection;
using ZooplaMatch.Application.Managers;
using ZooplaMatch.Domain.Models;
using ZooplaMatch.Domain.Models.Enums;
using ZooplaMatch.Models.Enums;

namespace ZooplaMatch.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services, string matchType, string numberOfPacks)
        {
            services.AddScoped(sp =>
            {
                return new GameRuleManger((CardMatchType)Enum.Parse(typeof(CardMatchType), matchType));
            });

            services.AddSingleton(sp =>
            {
                return new Deck(Convert.ToInt32(numberOfPacks));
            });

            services.AddScoped<IDeckManger, DeckManager>();

            services.AddSingleton(sp =>
            {
                return new GraveYard();
            });

            services.AddScoped<GraveYardManager>();

            services.AddScoped<IGameManager>(sp =>
            {
                var graveYardManager = sp.GetRequiredService<GraveYardManager>();
                var deckManager = sp.GetRequiredService<IDeckManger>();
                var gameRuleManager = sp.GetRequiredService<GameRuleManger>();
                var player1 = new Player(PlayerName.CPU1, new List<Card>(), new List<Card>());
                var player2 = new Player(PlayerName.CPU2, new List<Card>(), new List<Card>());

                return new GameManager(deckManager, gameRuleManager, graveYardManager, player1, player2);
            });
        }
    }
}
