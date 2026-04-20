using System.Text;
using Bombardino.Simulation;

namespace Bombardino.Renderer
{
    internal class Canvas
    {
        public readonly GameManager _gameManager;

        public char this[int i, int j]
        {
            get => _canvas[i, j];
            set => _canvas[i, j] = value;
        }

        private readonly StringBuilder _sb;
        private readonly char[,] _canvas;

        public int Width { get; }
        public int Height { get; }

        public Canvas(int size)
        {
            Width = size;
            Height = size / 2;
            _gameManager = GameManager.Instance;
            _canvas = new char[Height, Width];
            _sb = new StringBuilder();
        }

        //private char DrawPixel(int x, int y)
        //{
        //    int worldSize = _gameManager.CurrentGameWorld.Size;
        //    int uvX = x * worldSize / Width;
        //    int uvY = y * worldSize / Height;

        //    if (_gameManager.CurrentGameWorld.Positions2GameObjectsDict.ContainsKey(new Vec2<int>(uvX, uvY)))
        //    {
        //        return _gameManager.CurrentGameWorld.Positions2GameObjectsDict[new Vec2<int>(uvX, uvY)].Colour;
        //    }
        //    return ' ';
        //}

        public void DrawMovables(IMovable go)
        {
            int worldSize = _gameManager.GameplayGameWorld.Size;


            Vec2<float> position = go.PartialPosition;

            int uvX = (int) (position.x * Width)/worldSize;
            int uvY = (int) (position.y * Height)/worldSize;
            DrawGameObject(uvX, uvY, ((IGameObject)go).Sprite);
        }

        public void DrawGameObject(int x, int y, char[,] sprite)
        {
            for (int i = 0; i < sprite.GetLength(0); i++)
            {
                for (int j = 0; j < sprite.GetLength(1); j++)
                {
                    _canvas[i + y, j + x] = sprite[i, j];
                }
            }

        }

        public void OnUpdate()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    _canvas[y, x] = ' ';
                }
            }
            Parallel.ForEach(_gameManager.CurrentWorld.RealEstate, immovable =>
            {
                int worldSize = _gameManager.CurrentWorld.Size;

                int x = immovable.Position.x * Width / worldSize;
                int y = immovable.Position.y * Height / worldSize;
                DrawGameObject(x, y, immovable.Sprite);
            });

            foreach (var movable in _gameManager.CurrentWorld.Movables)
            {
                DrawMovables(movable);
            }
        }

        public string GetStringWBorder(string prependex = "")
        {
            _sb.Clear();
            _sb.Append("┌" + new string('─', Width) + "┐\n"); // Top
            for (int y = 0; y < Height; y++) // Main
            {
                _sb.Append(prependex);
                _sb.Append("|");
                for (int x = 0; x < Width; x++)
                {
                    _sb.Append(_canvas[y, x]);
                }
                _sb.Append("|\n");
            }

            _sb.Append("└" + new string('─', Width) + "┘"); // Bot

            return _sb.ToString();
        }
    }
}
