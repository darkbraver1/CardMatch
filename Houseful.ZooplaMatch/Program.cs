// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using ZooplaMatch.Application;
using ZooplaMatch.Application.Managers;

Console.WriteLine("Enter numbers of packs to play");
var numberOfPacks = Console.ReadLine();

Console.WriteLine("Choose match condition: 0 - Two matching suits, 1 - Two matching ranks, 2 - Matching Rank and Suit");

var matchCondition = Console.ReadLine();

Console.WriteLine(numberOfPacks);

var services = new ServiceCollection();

services.AddApplicationServices(matchCondition, numberOfPacks);

var di = services.BuildServiceProvider();

var gameManager = di.GetRequiredService<IGameManager>();

var graveYardManager = di.GetRequiredService<GraveYardManager>();

await gameManager.StartGame();

var cardsInGraveYard = graveYardManager.CardsIntheGraveYard();

Console.WriteLine(cardsInGraveYard);

var result = await gameManager.EndGame();

Console.WriteLine();

Console.WriteLine(result);
