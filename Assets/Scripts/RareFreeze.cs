namespace TagTag
{
    public class RareFreeze : IInteractable
    {
        private float _duration = 3f;

        public void Apply(Brain brain)
        {
            if (!brain) return;
            brain.ChangeMovementAvailableStatusForTime(_duration);
        }
    }
}