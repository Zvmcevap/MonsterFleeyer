namespace Bombardino
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StringHelpers.SanityCheck();
            GameManager gameManager = GameManager.Instance;
            bool success = gameManager.Initialize(100); // never gonna happen haha
            gameManager.Run();
            gameManager.Shutdown();
        }
    }
}
