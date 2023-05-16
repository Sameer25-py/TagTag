using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

namespace TagTag
{
    public class InteractionManager : MonoBehaviour
    {
        private                  Tilemap       _tileMap;
        public static            Action<Brain> IndexUpdated;
        [SerializeField] private Round         round;
        private                  int           _currentInteractable;

        private List<(InteractableData, IInteractable)> _instantiatedInteractables = new();
        private Dictionary<Vector3Int, IInteractable>   _interactablesMap          = new();

        private void OnEnable()
        {
            _tileMap     =  GetComponent<Tilemap>();
            IndexUpdated += OnBrainIndexUpdated;
        }

        private void InstantiateInteractables()
        {
            _interactablesMap.Clear();
            _currentInteractable = 0;
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
            (InteractableData data, IInteractable interactable) =
                _instantiatedInteractables[0];
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
                _tileMap.SetTile(randomIndex, round.AllowedInteractables[0]
                    .Sprite);
                Matrix4x4 scaleMatrix = Matrix4x4.Scale(data.scale);
                _tileMap.SetTransformMatrix(randomIndex, scaleMatrix);
                break;
            }
        }

        private void Start()
        {
            InstantiateInteractables();
            SpawnInteractable();
        }

        private void OnBrainIndexUpdated(Brain obj)
        {
            if (obj)
            {
                if (_interactablesMap.TryGetValue(obj.CurrentIndex, out IInteractable value))
                {
                    value.Apply(obj);
                    _interactablesMap[obj.CurrentIndex] = null;
                    
                    //todo: cache this interactable and index
                    _tileMap.SetTile(obj.CurrentIndex,null);
                }
            }
        }
        
        private void OnDisable()
        {
            IndexUpdated -= OnBrainIndexUpdated;
        }


        public void SetRound(Round currentRound)
        {
            round = currentRound;
        }
    }
}