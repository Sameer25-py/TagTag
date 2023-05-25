using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay
{
    public class InteractableManager : MonoBehaviour
    {
        private       List<Interactable>   _spawnedInteractables;
        public static Action<Interactable> InteractableDestroyed;

        public void DestroyAllInteractables()
        {
            foreach (Interactable i in _spawnedInteractables)
            {
                if (i)
                {
                    Destroy(i);
                }
            }
        }

        private void OnEnable()
        {
            InteractableDestroyed += OnInteractableDestroyed;
        }

        private void OnDisable()
        {
            InteractableDestroyed -= OnInteractableDestroyed;
        }

        private void OnInteractableDestroyed(Interactable obj)
        {
            _spawnedInteractables.Remove(obj);
        }
    }
}