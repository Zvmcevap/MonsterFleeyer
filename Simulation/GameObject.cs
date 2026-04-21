namespace Bombardino.Simulation
{
    public interface IGameObject
    {
        public Vec2<int> Position { get; set; }
        public char[,] Sprite { get; set; }
    }

    public interface IDestroyable
    {
        public int Health { get; set; }
        public bool Damage(int damage); // Return true if alive, false if dead
    }

    public interface IConsumable
    {
        public void Consume();
    }

    public interface IMovable
    {
        public Vec2<int> TargetPosition { get; set; }
        public Vec2<float> PartialPosition { get; set; }
        public Vec2<float> Velocity { get; set; }
        public float Speed { get; set; }
        public void Move(float dt);
    }

    public class Wall : IGameObject
    {
        public char[,] Sprite { get; set; }
        public Vec2<int> Position { get; set; }
        public Wall(Vec2<int> pos)
        {
            Position = pos;
            Sprite = StringHelpers.GetSpriteFromChar('W');
        }
    }

    public class Treasure : IGameObject, IConsumable
    {
        public char[,] Sprite { get; set; }
        public Vec2<int> Position { get; set; }

        public Treasure(Vec2<int> pos)
        {
            Position = pos;
            Sprite = StringHelpers.GetSpriteFromChar('T');
        }

        public void Consume()
        {
            GameManager.Instance.GameplayGameWorld.Positions2GameObjectsDict.Remove(Position);
        }

    }
}
