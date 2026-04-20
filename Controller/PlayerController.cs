namespace Bombardino.Controller
{
    public class PlayerController : ControllerBase
    {

        private readonly ConsoleKey _action;
        private readonly ConsoleKey _up;
        private readonly ConsoleKey _down;
        private readonly ConsoleKey _left;
        private readonly ConsoleKey _right;

        public PlayerController(IControllable target,
            ConsoleKey action = ConsoleKey.Spacebar,
            ConsoleKey up = ConsoleKey.W,
            ConsoleKey down = ConsoleKey.S,
            ConsoleKey left = ConsoleKey.A,
            ConsoleKey right = ConsoleKey.D) : base(target)
        {
            _up = up;
            _down = down;
            _left = left;
            _right = right;
            _action = action;
        }
        public override void UpdateControls()
        {
            while (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true).Key;

                if (key == _up) _target.HandleUp();
                else if (key == _down) _target.HandleDown();
                else if (key == _left) _target.HandleLeft();
                else if (key == _right) _target.HandleRight();
                else if (key == _action) _target.HandleAction();
                else if (key == ConsoleKey.Escape || key == ConsoleKey.Q)
                    _gameManager.QuitApplication();
            }
        }
    }
}
