using System.Diagnostics;
using System.Runtime.Serialization;
using Bombardino.Controller;
using Bombardino.Renderer;
using Bombardino.Simulation;

namespace Bombardino
{
    public sealed class GameManager
    {
        public static float DeltaTime { get; private set; }

        private Canvas _canvas;
        public int BellowCanvas => _canvas.Height + 5;

        #region Gameplay
        private int _winCounter;
        private int _looseCounter;

        public float PlayerSpeed = 5f;
        public float MonsterSpeed = 5f;

        public bool CurrentlyPlaying;
        public GamePlayWorld GameplayGameWorld { get; private set; }
        public MenuWorld MenuScreen { get; private set; }
        public GameWorld CurrentWorld { get; private set; }

        #endregion

        #region Game State
        private bool IsRunning;
        #endregion

        #region Singleton stuff
        private static GameManager? _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }


        private GameManager() { }

        #endregion

        public bool Initialize(int size)
        {
            Console.CursorVisible = false;

            // Maximum window size minus buffor or smaller
            int width = Math.Min(size, Console.LargestWindowWidth - 10);
            int height = Math.Min(size, Console.LargestWindowHeight - 10);
            // Create the canvas, "gameplay" window
            _canvas = new Canvas(size);
            try
            {
                Console.SetWindowSize(Math.Max(width + 10, 100), Math.Max(height + 10, 25));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            GameplayGameWorld = new GamePlayWorld(10);
            MenuScreen = new MenuWorld(10);
            CurrentWorld = MenuScreen;

            return true;
        }
        public void Shutdown()
        {
            IsRunning = false;
            Console.CursorVisible = true;
        }

        public void Run()
        {
            IsRunning = true;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.Clear();
            MenuScreen.ShowStartScreen();
            while (IsRunning)
            {
                DeltaTime = (float)stopwatch.Elapsed.TotalSeconds;
                $"FPS = {(int)(1f / DeltaTime)}".PrintOnScreen(0, 0);
                stopwatch.Restart();
                CurrentWorld.UpdateControls();

                if (CurrentlyPlaying) GameplayGameWorld.UpdateGame(DeltaTime);

                Draw();
            }
        }

        public void StartNewGame()
        {
            CurrentlyPlaying = true;
            GameplayGameWorld.Initialize(PlayerSpeed, MonsterSpeed);
            CurrentWorld = GameplayGameWorld;
        }

        public void WinGame()
        {
            ++_winCounter;
            CurrentlyPlaying = false;
            CurrentWorld = MenuScreen;
            MenuScreen.ShowWinScreen();

        }

        public void LooseGame()
        {
            ++_looseCounter;
            CurrentlyPlaying = false;
            CurrentWorld = MenuScreen;
            MenuScreen.ShowLooseScreen();
        }

        public void QuitApplication()
        {
            if (CurrentlyPlaying) 
            {
                LooseGame();
                return;
            }
            IsRunning = false;
        }

        private void Draw()
        {
            $"Wins   = {_winCounter}".PrintOnScreen(0, 1);
            $"Losses = {_looseCounter}".PrintOnScreen(0, 2);
            DrawCanvas();
        }

        private void DrawCanvas()
        {
            _canvas.OnUpdate();
            Console.SetCursorPosition(0, 3);
            string canvaString = _canvas.GetStringWBorder();
            Console.Write(canvaString);
        }
    }
}
