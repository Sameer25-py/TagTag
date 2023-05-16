using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TagTag
{
    public class InteractionManager : MonoBehaviour
    {
        private                  Tilemap       _tileMap;
        public static            Action<Brain> IndexUpdated;
        [SerializeField] private Round         round;

        private List<Interactable>                   _instantiatedInteractables = new();
        private Dictionary<Vector3Int, Interactable> _interactablesDistribution = new();

        private void OnEnable()
        {
            _tileMap     =  GetComponent<Tilemap>();
            IndexUpdated += OnBrainIndexUpdated;
        }

        private void InstantiateInteractables()
        {
            _instantiatedInteractables = new();
            for (int i = 0; i < round.AllowedInteractables.Count; i++)
            {
                for (int j = 0;
                     j < round.AllowedInteractables[i]
                         .SpawnCount;
                     j++)
                {
                    if (i == 0)
                    {
                        _instantiatedInteractables.Add(new RareCircle());
                    }

                    else if (i == 1)
                    {
                        _instantiatedInteractables.Add(new RareArrow());
                    }

                    else if (i == 2)
                    {
                        _instantiatedInteractables.Add(new RareFreeze());
                    }
                }
            }
        }

        private void CreateInteracblesDistribution()
        {
            
        }

        private void CreateInteractableDistribution(AllowedInteractable data, Interactable interactable)
        {
            interactable.SpawnInteractable(_tileMap, data.InteractableData);
        }

        private void Start()
        {   
            InstantiateInteractables();
            CreateInteracblesDistribution();
        }

        private void OnBrainIndexUpdated(Brain obj) { }

        private void OnDisable() { }

        private void SpawnInteractable() { }

        public void SetRound(Round currentRound)
        {
            round = currentRound;
        }
    }

    [Serializable]
    public class InteractablesMap { }
}