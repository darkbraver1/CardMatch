namespace ZooplaMatch.Application.Managers
{
    public interface IGameManager
    {
        Task StartGame();

        Task<string> EndGame();
    }
}
