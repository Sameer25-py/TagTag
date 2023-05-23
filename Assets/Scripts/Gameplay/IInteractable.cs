using Gameplay.Grid;
using UnityEngine;

namespace Gameplay
{
    public interface IInteractable
    {
        public bool Apply(Brain brain,AudioClip effectSound);
    }
}