using UnityEngine;

namespace Gameplay.Grid
{
    public class RareFreeze : IInteractable
    {
        private float _duration = 3f;

        public bool Apply(Brain brain, AudioClip effectSound)
        {
            if (!brain) return false;
            AudioManager.PlaySound(effectSound);
            brain.ChangeMovementAvailableStatusForTime(_duration);
            return true;
        }
    }
}