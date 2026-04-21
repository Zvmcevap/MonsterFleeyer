using Bombardino.Controller;

namespace Bombardino.Simulation
{
    public class Character : IGameObject, IMovable, IDestroyable, IControllable
    {
        private GameManager _gameManager;
        private bool _winOnNextTile;

        public bool IsPlayer { get; private set; }
        public char[,] Sprite { get; set; }

        public Vec2<int> Position { get; set; }
        public Vec2<int> TargetPosition { get; set; }
        public Vec2<float> PartialPosition { get; set; }
        public Vec2<float> Velocity { get; set; }

        public float MovedAmount { get; private set; }
        public float Speed { get; set; }
        public int Health { get; set; }

        public Character(Vec2<int> position, float speed, int health, char[,] sprite, bool isPlayer)
        {
            _gameManager = GameManager.Instance;

            Position = position;
            Speed = speed;
            PartialPosition = new Vec2<float>((float)Position.x, (float)Position.y);
            Health = health;
            Sprite = sprite;
            IsPlayer = isPlayer;
        }

        public void SetPosition(Vec2<int> inPos)
        {
            Position = inPos;
            PartialPosition = new Vec2<float>((float)Position.x, (float)Position.y);
        }

        public bool Damage(int damage)
        {
            Health -= damage;
            if (Health <= 0) return false;
            return true;
        }

        public void Move(float dt)
        {
            if (Velocity == Vec2<float>.Zero) return;

            PartialPosition += Velocity * Speed * dt;
            MovedAmount += dt * Speed;

            if (MovedAmount >= 1f)
            {
                if (_winOnNextTile)
                {
                    _gameManager.WinGame();
                }
                Velocity = Vec2<float>.Zero;
                MovedAmount = 0f;

                var posGODict = _gameManager.GameplayGameWorld.Positions2GameObjectsDict;
                posGODict.Remove(Position);
                posGODict[TargetPosition] = this;

                Position = TargetPosition;
                PartialPosition = new Vec2<float>((float)Position.x, (float)Position.y);
            }
        }

        public void Reset(Vec2<int> pos, int health, float speed)
        {
            _winOnNextTile = false;
            Health = health;
            Speed = speed;
            Velocity = Vec2<float>.Zero;
            SetPosition(pos);
        }

        public void HandleAction()
        {
            _gameManager.CurrentlyPlaying = !_gameManager.CurrentlyPlaying;
        }

        public void HandleUp()
        {
            if (Velocity != Vec2<float>.Zero) return;
            var dir = Vec2<int>.Up;

            if (!CheckAvailability(dir)) return;

            Velocity = Vec2<float>.Up;
            TargetPosition = Position + dir;
        }
        public void HandleDown()
        {
            if (Velocity != Vec2<float>.Zero) return;

            var dir = Vec2<int>.Down;
            if (!CheckAvailability(dir)) return;
            Velocity = Vec2<float>.Down;
            TargetPosition = Position + dir;
        }
        public void HandleLeft()
        {
            if (Velocity != Vec2<float>.Zero) return;

            var dir = Vec2<int>.Left;
            if (!CheckAvailability(dir)) return;

            Velocity = Vec2<float>.Left;
            TargetPosition = Position + dir;
        }
        public void HandleRight()
        {
            if (Velocity != Vec2<float>.Zero) return;

            var dir = Vec2<int>.Right;
            if (!CheckAvailability(dir)) return;

            Velocity = Vec2<float>.Right;
            TargetPosition = Position + dir;
        }

        bool CheckAvailability(Vec2<int> direction)
        {
            GamePlayWorld gw = _gameManager.GameplayGameWorld;
            Vec2<int> newPosition = Position + direction;

            if (newPosition.x < 0 || newPosition.y < 0 || newPosition.x >= gw.Size || newPosition.y >= gw.Size) return false;

            gw.Positions2GameObjectsDict.TryGetValue(newPosition, out IGameObject? blocker);

            if (blocker != null && blocker is not IMovable && blocker is not IConsumable)
            {
                    return false;
            }
            gw.Positions2GameObjectsDict[newPosition] = this;
            return true;
        }
    }
}
