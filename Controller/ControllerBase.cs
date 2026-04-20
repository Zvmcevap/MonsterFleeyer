using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombardino.Controller
{
    /// <summary>
    /// Controller for both player and monster to inherit
    /// </summary>
    public abstract class ControllerBase
    {
        internal readonly GameManager _gameManager = GameManager.Instance;
        internal IControllable _target { get; private set; }

        public ControllerBase(IControllable target)
        {
            _target = target;
        }

        internal void SetTarget(IControllable target)
        {
            _target = target;
        }
        public abstract void UpdateControls();

    }
}
