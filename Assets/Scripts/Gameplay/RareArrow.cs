namespace Gameplay
{
    public class RareArrow : IInteractable
    {
        private float _speedFactor = 0.5f;
        private float _duration    = 1.5f;

        public bool Apply(Brain brain)
        {
            if (!brain) return false;
            brain.ChangeMovementSpeedForTime(_speedFactor, _duration);
            return true;
        }
    }
}