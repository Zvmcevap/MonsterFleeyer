using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Bombardino.Simulation;

namespace Bombardino.Controller
{
    public class MonsterController : ControllerBase
    {
        private static Vec2<int>[] DIRS = [Vec2<int>.Up, Vec2<int>.Down, Vec2<int>.Left, Vec2<int>.Right];
        static Random rnd = new Random();
        private readonly List<Action> _decisions;
        private List<Vec2<int>> _pathToTreasure = new();
        public MonsterController(IControllable target) : base(target)
        {
            _decisions = [Target.HandleUp, Target.HandleDown, Target.HandleLeft, Target.HandleRight];
        }

        public override void UpdateControls()
        {
            Character ch = (Character)Target;

            if (ch.Velocity != Vec2<float>.Zero) return;

            if ( _pathToTreasure.Count > 0)
            {
                ch.TargetPosition = _pathToTreasure[0];
                _pathToTreasure.Remove(ch.TargetPosition);
                Vec2<int> dir = ch.TargetPosition - ch.Position;
                if (dir == Vec2<int>.Zero) return;

                int index = Array.FindIndex(DIRS, x => x == dir);
                _decisions[index]();
            }
            int choice = rnd.Next(_decisions.Count);
            _decisions[choice]();
        }
        public void Reset()
        {
            _pathToTreasure.Clear();
        }
        public bool FindPathToTreasure()
        {
            var gw = _gameManager.GameplayGameWorld;
            var start = ((Character)Target).Position;

            PriorityQueue<Vec2<int>, int> pq = new();
            HashSet<Vec2<int>> visited = new();
            Dictionary<Vec2<int>, Vec2<int>> cameFrom = new();

            pq.Enqueue(start, 0);
            visited.Add(start);

            while (pq.Count > 0)
            {
                pq.TryDequeue(out var pos, out var priority);

                gw.Positions2GameObjectsDict.TryGetValue(pos, out IGameObject? obj);
                if (obj is IConsumable)
                {
                    _pathToTreasure = ReconstructPath(start, pos, cameFrom);
                    return true;
                }

                foreach (var dir in DIRS)
                {
                    var neighbor = pos + dir;

                    if (!visited.Contains(neighbor) && CheckAvailability(neighbor))
                    {
                        visited.Add(neighbor);
                        cameFrom[neighbor] = pos;
                        pq.Enqueue(neighbor, priority + 1);
                    }
                }
            }

            return false;
        }

        private static List<Vec2<int>> ReconstructPath(
            Vec2<int> start,
            Vec2<int> goal,
            Dictionary<Vec2<int>, Vec2<int>> cameFrom)
        {
            List<Vec2<int>> path = new();
            var current = goal;

            path.Add(current);

            while (!current.Equals(start))
            {
                current = cameFrom[current];
                path.Add(current);
            }

            path.Reverse();
            return path;
        }
        public bool CheckAvailability(Vec2<int> newPosition)
        {
            var gw = _gameManager.GameplayGameWorld;

            if (newPosition.x < 0 || newPosition.y < 0 || newPosition.x >= gw.Size || newPosition.y >= gw.Size) return false;

            gw.Positions2GameObjectsDict.TryGetValue(newPosition, out IGameObject? blocker);

            if (blocker != null && blocker is not IConsumable)
            {
                return false;
            }
            return true;
        }
    }
}
