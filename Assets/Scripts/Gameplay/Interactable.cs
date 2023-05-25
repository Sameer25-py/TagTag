using System;
using Gameplay.Grid;
using UnityEngine;

namespace Gameplay
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private InteractableData data;

        protected virtual void ApplyEffect(Character c)
        {
            AudioManager.PlaySound(data.EffectClip);
            Destroy(gameObject);
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Character c))
            {
                ApplyEffect(c);
            }
        }

        private void OnDestroy()
        {
            InteractableManager.InteractableDestroyed?.Invoke(this);
        }
    }
}