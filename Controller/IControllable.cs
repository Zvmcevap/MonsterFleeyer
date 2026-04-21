
using Bombardino.Simulation;

namespace Bombardino.Controller
{
    /// <summary>
    /// Game will support 4 directions and one action
    /// </summary>
    public interface IControllable
    {
        public void HandleAction();
        public void HandleUp();
        public void HandleDown();
        public void HandleLeft();
        public void HandleRight();
    }
    public class MenuHandler : IControllable
    {
        private bool _playerSpeed = true;

        public MenuHandler()
        {
            PrintStatusStrings();
        }

        public void HandleAction()
        {
            if (GameManager.Instance.MenuScreen.CurrentScreen != MenuWorld.EMenuScreens.Start)
            {
                GameManager.Instance.MenuScreen.ShowStartScreen();
            }
            else
            {
                GameManager.Instance.StartNewGame();
            }
        }

        public void HandleDown()
        {
            SwitchMode();
        }

        public void HandleLeft()
        {
            ChangeSpeed(-1f);
        }

        public void HandleRight()
        {
            ChangeSpeed(1f);
        }

        public void HandleUp()
        {
            SwitchMode();
        }

        private void SwitchMode()
        {
            _playerSpeed = !_playerSpeed;
            PrintStatusStrings();
        }

        private void ChangeSpeed(float delta)
        {
            if (_playerSpeed)
            {
                float change = GameManager.Instance.PlayerSpeed + delta;
                GameManager.Instance.PlayerSpeed = Math.Clamp(change, 1f, 10f);
            }
            else
            {
                float change = GameManager.Instance.MonsterSpeed + delta;
                GameManager.Instance.MonsterSpeed = Math.Clamp(change, 1f, 10f);
            }
            PrintStatusStrings();
        }

        public void PrintStatusStrings()
        {
            if (_playerSpeed)
            {
                $"[PLAYER SPEED]   = < {GameManager.Instance.PlayerSpeed}  >".PrintBellowCanvas(0);
                $" MONSTER SPEED   =   {GameManager.Instance.MonsterSpeed}".PrintBellowCanvas(1);
            }
            else
            {
                $" PLAYER SPEED    =   {GameManager.Instance.PlayerSpeed}".PrintBellowCanvas(0);
                $"[MONSTER SPEED]  = < {GameManager.Instance.MonsterSpeed} >".PrintBellowCanvas(1);
            }
        }
    }
}
