using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombardino.Controller
{
    public class MonsterController : ControllerBase
    {
        static Random rnd = new Random();
        private readonly List<Action> _decisions;
        public MonsterController(IControllable target) : base(target)
        {
            _decisions = [_target.HandleAction, _target.HandleUp, _target.HandleDown, _target.HandleLeft, _target.HandleRight];
        }

        public override void UpdateControls()
        {
            int choice = rnd.Next(_decisions.Count);
            _decisions[choice]();
        }
    }
}
