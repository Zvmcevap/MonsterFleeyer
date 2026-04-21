using System.Runtime.CompilerServices;
using Bombardino.Controller;

namespace Bombardino.Simulation
{
    public abstract class GameWorld
    {
        public int Size;
        internal PlayerController _playerController { get; set; }
        public Dictionary<Vec2<int>, IGameObject> Positions2GameObjectsDict { get; } = new();
        public List<IMovable> Movables { get; internal set; } = new();
        public List<IGameObject> RealEstate { get; internal set; } = new();

        public readonly List<IGameObject> GameObjects = new();
        public GameWorld(int size)
        {
            Size = size;
        }

        public abstract void UpdateControls();
    }

    public class MenuWorld : GameWorld
    {
        public enum EMenuScreens
        {
            Start = 0,
            Win = 1,
            Loose = 2
        }

        public EMenuScreens CurrentScreen { get; private set; }
        private readonly List<IGameObject>[] _menuScreens =  [new(), new(), new()];

        public MenuWorld(int size) : base(size)
        {
            _playerController = new PlayerController(new MenuHandler());
            CreateScreens();
            ShowStartScreen();
        }

        public override void UpdateControls()
        {
            _playerController.UpdateControls();
        }

        public void ShowStartScreen()
        {
            CurrentScreen = EMenuScreens.Start;
            RealEstate = _menuScreens[(int)EMenuScreens.Start];
            ((MenuHandler)_playerController.Target).PrintStatusStrings();
        }
        public void ShowWinScreen()
        {
            CurrentScreen = EMenuScreens.Win;
            RealEstate = _menuScreens[(int)EMenuScreens.Win];
        }
        public void ShowLooseScreen()
        {
            CurrentScreen = EMenuScreens.Loose;
            RealEstate = _menuScreens[(int)EMenuScreens.Loose];
        }

        private void CreateScreens()
        {
            RealEstate.Clear();
            Movables.Clear();

            string spacebar =      " SPACEBAR ";
            string to =            "    TO    ";
            string start =         "   START  ";
            string congrats =      " CONGRATS ";
            string you =           "    YOU   ";
            string win =           "    WIN   ";
            string sorry =         "   SORRY  ";
            string loose =         "   LOOSE  ";

            string[] startScreens = [spacebar, to, start];
            string[] winscreens = [congrats, you, win];
            string[] looseScreens = [sorry, you, loose];

            string[][] allscreens = [startScreens, winscreens, looseScreens];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var pos = new Vec2<int>(j, i*2 + 2);
                    foreach (EMenuScreens screen in Enum.GetValues(typeof(EMenuScreens)))
                    {
                        char startC = allscreens[(int)screen][i][j];

                        if (StringHelpers.Char2String.ContainsKey(startC))
                        {
                            _menuScreens[(int)screen].Add(new Wall(pos) { Sprite = StringHelpers.GetSpriteFromChar(startC) });
                        }
                    }
                }
            }
        }
    }

    public class GamePlayWorld : GameWorld
    {
        private readonly Character _player;
        private readonly Character _monster;
        private readonly MonsterController _monsterController;

        private const int PLAYER_HEALTH = 2;
        private const float GRACE_PERIOD = 1f;
        private float _gracePeriodTimer = 0f;
        private bool _hit = false;
        private Treasure _treasure;

        public GamePlayWorld(int size) : base(size)
        {
            _player = new Character(new Vec2<int>(0, 0), 1f, PLAYER_HEALTH, StringHelpers.GetSpriteFromChar('P'), true);
            _playerController = new PlayerController(_player);
            Movables.Add(_player);

            _monster = new Character(new Vec2<int>(size - 1, size - 1), 1f, 1, StringHelpers.GetSpriteFromChar('M'), false);
            _monsterController = new MonsterController(_monster);
            Movables.Add(_monster);
        }

        public void Initialize(float playerSpeed, float monsterSpeed)
        {
            _hit = false;
            _player.Reset(Vec2<int>.Zero, PLAYER_HEALTH, playerSpeed);
            _player.Sprite = StringHelpers.PlayerSprite;
            _monster.Reset(Vec2<int>.One * (Size - 1), 1, monsterSpeed);
            _monsterController.Reset();
            PlaceImmovables();
            while (!_monsterController.FindPathToTreasure())
            {
                PlaceImmovables();
            }

            Positions2GameObjectsDict[_player.Position] = _player;
            Positions2GameObjectsDict[_monster.Position] = _monster;
        }

        private void PlaceImmovables()
        {
            Positions2GameObjectsDict.Clear();
            RealEstate.Clear();
            List<Vec2<int>> validPositions = new();
            for (int i = 1; i < Size - 2; i++)
            {
                for (int j = 0; j < Size - 1; j++)
                {
                    validPositions.Add(new Vec2<int>(i, j));
                }
            }
            int numWalls = 15;
            var rng = new Random();
            for (int i = 0; i < numWalls; i++)
            {
                int index = rng.Next(validPositions.Count);
                var pos = validPositions[index];
                validPositions.Remove(pos);
                var wall = new Wall(pos);
                Positions2GameObjectsDict[wall.Position] = wall;
                RealEstate.Add(wall);
            }
            int treasureIndex = rng.Next(validPositions.Count);
            var treasurePos = validPositions[treasureIndex];
            validPositions.Remove(treasurePos);
            var treasure = new Treasure(treasurePos);
            _treasure = treasure;
            Positions2GameObjectsDict[treasure.Position] = treasure;
            RealEstate.Add(treasure);
        }

        public override void UpdateControls()
        {
            _playerController.UpdateControls();
            _monsterController.UpdateControls();
        }

        public void UpdateGame(float dt)
        {
            Parallel.ForEach(Movables, mover => mover.Move(dt));

            if (_player.Position == _treasure.Position)
            {
                GameManager.Instance.WinGame();
            }

            if (_hit)
            {
                _gracePeriodTimer += dt;
                $"HIT! Invornable for {GRACE_PERIOD - _gracePeriodTimer}".PrintBellowCanvas(1);
                if (_gracePeriodTimer > GRACE_PERIOD)
                {
                    _hit = false;
                    _gracePeriodTimer = 0f;
                    _player.Sprite = StringHelpers.PlayerSprite;
                }
            }
            else
            {
                CheckCollision();
                "".PrintBellowCanvas(1);
            }
            $"HEALTH = {_player.Health}".PrintBellowCanvas(0);
        }

        private void CheckCollision()
        {
            Vec2<float> halfsies = new Vec2<float>(0.5f, 0.5f);
            if (Vec2<float>.DistanceSqr(_player.PartialPosition + halfsies, _monster.PartialPosition + halfsies) < 1f)
            {
                if (!_player.Damage(1))
                {
                    GameManager.Instance.LooseGame();
                }
                _player.Sprite = StringHelpers.InvornablePlayerSprite;
                _hit = true;
            }
        }
    }
}
