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
        public IControllable Target { get; private set; }

        public ControllerBase(IControllable target)
        {
            Target = target;
        }

        internal void SetTarget(IControllable target)
        {
            Target = target;
        }
        public abstract void UpdateControls();

    }
}
