using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gameplay
{
    public class InteractionManager : MonoBehaviour
    {
        private                  Tilemap       _tileMap;
        public static            Action<Brain> IndexUpdated;
        [SerializeField] private Round         round;

        private List<(InteractableData, IInteractable)> _instantiatedInteractables = new();
        private Dictionary<Vector3Int, IInteractable>   _interactablesMap          = new();

        private void OnEnable()
        {
            _tileMap              =  GetComponent<Tilemap>();
            IndexUpdated          += OnBrainIndexUpdated;
            Timer.TimerProgressed += OnTimerProgressed;
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

        private void InstantiateInteractables()
        {
            _interactablesMap.Clear();
            _instantiatedInteractables.Clear();
            for (int i = 0; i < round.AllowedInteractables.Count; i++)
            {
                for (int j = 0;
                     j < round.AllowedInteractables[i]
                         .SpawnCount;
                     j++)
                {
                    if (round.AllowedInteractables[i]
                            .Name == "Circle")
                    {
                        _instantiatedInteractables.Add((round.AllowedInteractables[i], new RareCircle()));
                    }

                    else if (round.AllowedInteractables[i]
                                 .Name == "Arrow")
                    {
                        _instantiatedInteractables.Add((round.AllowedInteractables[i], new RareArrow()));
                    }

                    else if (round.AllowedInteractables[i]
                                 .Name == "Freeze")
                    {
                        _instantiatedInteractables.Add((round.AllowedInteractables[i], new RareFreeze()));
                    }
                }
            }
        }

        private void SpawnInteractable()
        {
            if (_instantiatedInteractables.Count == 0) return;
            (InteractableData data, IInteractable interactable) =
                _instantiatedInteractables[UnityEngine.Random.Range(0, _instantiatedInteractables.Count)];
            if (interactable is null) return;
            AddInteractableToMap(data, interactable);
            _instantiatedInteractables.Remove((data, interactable));
        }

        private void AddInteractableToMap(InteractableData data, IInteractable interactable, int noOfRetries = 5)
        {
            for (int i = 0; i < noOfRetries; i++)
            {
                Vector3Int randomIndex = Grid.GetRandomValidIndex();
                if (_interactablesMap.ContainsKey(randomIndex)) continue;
                _interactablesMap[randomIndex] = interactable;
                _tileMap.SetTile(randomIndex, data.Sprite);
                Matrix4x4 scaleMatrix = Matrix4x4.Scale(data.scale);
                _tileMap.SetTransformMatrix(randomIndex, scaleMatrix);
                break;
            }
        }

        private void OnBrainIndexUpdated(Brain obj)
        {
            if (obj)
            {
                if (_interactablesMap.TryGetValue(obj.CurrentIndex, out IInteractable value))
                {
                    if (value is not null)
                    {
                        bool isEffectApplied = value.Apply(obj);
                        if (!isEffectApplied) return;
                        _interactablesMap[obj.CurrentIndex] = null;

                        //todo: cache this interactable and index
                        _tileMap.SetTile(obj.CurrentIndex, null);
                    }
                }
            }
        }

        private void OnDisable()
        {
            IndexUpdated          -= OnBrainIndexUpdated;
            Timer.TimerProgressed -= OnTimerProgressed;
        }

        public void SetRound(Round currentRound)
        {
            round = currentRound;
            RemoveInteractables();
            InstantiateInteractables();
        }

        private void RemoveInteractables()
        {
            foreach (var key in _interactablesMap.Keys)
            {
                _tileMap.SetTile(key, null);
            }
        }
        
    }
}