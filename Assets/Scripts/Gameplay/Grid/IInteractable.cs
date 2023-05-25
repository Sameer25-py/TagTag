using UnityEngine;

namespace Gameplay.Grid
{
    public interface IInteractable
    {
        public bool Apply(Brain brain,AudioClip effectSound);
    }
}