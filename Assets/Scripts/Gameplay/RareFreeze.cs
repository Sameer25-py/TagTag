using UnityEngine;

namespace Gameplay
{
    public class RareFreeze : Interactable
    {
        [SerializeField] private float time;

        protected override void ApplyEffect(Character c)
        {
            c.ChangeEnableMovemntForTime(time, false);
            base.ApplyEffect(c);
        }
    }
}