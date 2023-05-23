using Gameplay.Grid;
using UnityEngine;

namespace Gameplay
{
    public class RareCircle : IInteractable
    {
        public bool Apply(Brain brain, AudioClip effectSound)
        {
            if (!brain) return false;
            if (!brain.TryGetComponent(out InfectedCollider _)) return false;
            if (!brain.TryGetComponent(out Gameplay.Grid.Character character)) return false;
            AudioManager.PlaySound(effectSound);
            character.UnInfectCharacter();
            BrainManager.InfectRandomBrain(brain);
            return true;
        }
    }
}