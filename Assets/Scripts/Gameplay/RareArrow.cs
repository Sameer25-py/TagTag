using Gameplay.Grid;
using UnityEngine;

namespace Gameplay
{
    public class RareArrow : IInteractable
    {   
        
        private float _speedFactor = 0.5f;
        private float _duration    = 1.5f;
        

        public bool Apply(Brain brain, AudioClip effectSound)
        {
            if (!brain) return false;
            AudioManager.PlaySound(effectSound);
            brain.ChangeMovementSpeedForTime(_speedFactor, _duration);
            return true;
        }
    }
}