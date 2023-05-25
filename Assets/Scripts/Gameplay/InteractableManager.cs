using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class InteractableManager : MonoBehaviour
    {
        private       List<Interactable>     _spawnedInteractables = new();
        private       List<InteractableData> _interactableDatas    = new();
        public static Action<Interactable>   InteractableDestroyed;


        public void Initialize(Round round)
        {
            DestroyAllInteractables();
            InitializeInteractables(round);
        }

        private void DestroyAllInteractables()
        {
            foreach (Interactable i in _spawnedInteractables)
            {
                if (i)
                {
                    Destroy(i.gameObject);
                }
            }

            _spawnedInteractables = new();
        }

        private void InitializeInteractables(Round round)
        {
            _interactableDatas = new();
            foreach (InteractableData allowedInteractable in round.AllowedInteractables)
            {
                for (int i = 0; i < allowedInteractable.SpawnCount; i++)
                {
                    _interactableDatas.Add(allowedInteractable);
                }
            }
        }

        private void SpawnInteractable()
        {
            if (_interactableDatas.Count == 0) return;
            InteractableData randomInteractable = _interactableDatas[Random.Range(0, _interactableDatas.Count)];
            _interactableDatas.Remove(randomInteractable);

            Vector2 randomSpawnPoint = RandomPointGenerator.GetRandomPointOnMap(0.6f);
            GameObject instantiatedInteractable = Instantiate(randomInteractable.Prefab, randomSpawnPoint,
                Quaternion.identity);
            _spawnedInteractables.Add(instantiatedInteractable.GetComponent<Interactable>());
        }

        private void OnTimerProgressed(float obj)
        {
            if (obj == 0.1f)
            {
                SpawnInteractable();
            }
            else if (obj == 0.3f)
            {
                SpawnInteractable();
            }

            else if (obj == 0.5)
            {
                SpawnInteractable();
            }

            else if (obj == 0.8f)
            {
                SpawnInteractable();
                SpawnInteractable();
            }

            else if (obj == 0.9f)
            {
                SpawnInteractable();
            }
        }

        private void OnEnable()
        {
            InteractableDestroyed          += OnInteractableDestroyed;
            Gameplay.Timer.TimerProgressed += OnTimerProgressed;
        }

        private void OnDisable()
        {
            InteractableDestroyed          -= OnInteractableDestroyed;
            Gameplay.Timer.TimerProgressed -= OnTimerProgressed;
        }

        private void OnInteractableDestroyed(Interactable obj)
        {
            _spawnedInteractables.Remove(obj);
        }
    }
}