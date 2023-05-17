namespace TagTag
{
    public class RareFreeze : IInteractable
    {
        private float _duration = 3f;

        public bool Apply(Brain brain)
        {
            if (!brain) return false;
            brain.ChangeMovementAvailableStatusForTime(_duration);
            return true;
        }
    }
}