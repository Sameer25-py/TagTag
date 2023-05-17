using System.Collections;

namespace TagTag
{
    public class RareArrow : IInteractable
    {
        private float _speedFactor = 0.5f;
        private float _duration    = 1.5f;

        public void Apply(Brain brain)
        {
            if (!brain) return;
            brain.ChangeMovementSpeedForTime(_speedFactor, _duration);
        }
    }
}