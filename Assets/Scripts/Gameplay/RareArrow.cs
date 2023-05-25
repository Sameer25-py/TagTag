using UnityEngine;

namespace Gameplay
{
    public class RareArrow : Interactable
    {
        [SerializeField] private float speed = 2f;
        [SerializeField] private float time  = 2f;

        protected override void ApplyEffect(Character c)
        {
            c.ChangeSpeedForTime(speed, time);
            base.ApplyEffect(c);
        }
    }
}