using UnityEngine;

namespace Gameplay
{
    public class RareCircle : Interactable
    {
        protected override void ApplyEffect(Character c)
        {
            if (c.IsInfected)
            {
                c.UnInfectCharacter();
                CharacterManager.InfectRandomCharacterExcept?.Invoke(c);
                base.ApplyEffect(c);
            }
        }
    }
}