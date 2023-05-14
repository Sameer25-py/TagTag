using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TagTag
{
    public class BrainManager : MonoBehaviour
    {
        [SerializeField] private List<Brain> Brains;
        [SerializeField] private int         randomIndexTries = 5;

        public static Action<AI>    DestinationReached;
        public static Action<Brain> BrainDestroyed;
        public static Action<Brain> UpdateInfectedBrain;

        public Brain InfectedBrain;

        private Vector3Int GetRandomValidIndex()
        {
            for (int i = 0; i < randomIndexTries; i++)
            {
                Vector3Int randomIndex = GridInfo.GetRandomIndexInGrid();
                if (Grid.CheckGridIndex(randomIndex))
                {
                    return randomIndex;
                }
            }

            return new();
        }

        private void PlaceCharactersAtValidIndices()
        {
            List<Vector3Int> cachedIndices = new();
            foreach (Brain brain in Brains)
            {
                for (int i = 0; i < randomIndexTries; i++)
                {
                    Vector3Int randomIndex = GridInfo.GetRandomIndexInGrid();
                    if (Grid.CheckGridIndex(randomIndex) && !cachedIndices.Contains(randomIndex))
                    {
                        cachedIndices.Add(randomIndex);
                        brain.MoveCharacterToPosition(Grid.TileMap.CellToWorld(randomIndex));
                        break;
                    }
                }
            }
        }

        private void AssignAIBrainDestinations()
        {
            foreach (Brain brain in Brains)
            {
                if (brain is AI aiBrain)
                {
                    if (brain != InfectedBrain)
                    {
                        aiBrain.SetTarget(GetRandomValidIndex());
                    }
                }
            }

            if (InfectedBrain is not AI ai) return;
            {
                List<Brain> nonInfectedBrains = new();
                foreach (Brain brain in Brains)
                {
                    if (brain != InfectedBrain)
                    {
                        nonInfectedBrains.Add(brain);
                    }
                }

                ai.SetTarget(nonInfectedBrains[Random.Range(0, nonInfectedBrains.Count)]
                    .CurrentIndex);
            }
        }

        private void SetRandomBrainToInfect()
        {
            // Brains[Random.Range(0, Brains.Count)]
            //     .InfectBrain();
            
            Brains[0].InfectBrain();
        }

        private void OnEnable()
        {
            DestinationReached  += OnAITargetReached;
            BrainDestroyed      += OnBrainDestroyed;
            UpdateInfectedBrain += OnInfectedBrainUpdated;
        }

        private void OnInfectedBrainUpdated(Brain obj)
        {
            if (obj)
            {
                InfectedBrain = obj;
            }
        }

        private void OnBrainDestroyed(Brain obj)
        {
            if (Brains.Contains(obj))
            {
                Brains.Remove(obj);
            }
        }

        private void Start()
        {
            PlaceCharactersAtValidIndices();
            SetRandomBrainToInfect();
            AssignAIBrainDestinations();
        }

        private void OnDisable()
        {
            DestinationReached  -= OnAITargetReached;
            BrainDestroyed      -= OnBrainDestroyed;
            UpdateInfectedBrain -= OnInfectedBrainUpdated;
        }

        private void OnAITargetReached(AI obj)
        {
            if (!obj) return;
            if (obj != InfectedBrain)
            {
                obj.SetTarget(GetRandomValidIndex());
            }
            else
            {
                List<Brain> nonInfectedBrains = new();
                foreach (Brain brain in Brains)
                {
                    if (brain != InfectedBrain)
                    {
                        nonInfectedBrains.Add(brain);
                    }
                }

                obj.SetTarget(nonInfectedBrains[Random.Range(0, nonInfectedBrains.Count)]
                    .CurrentIndex);
            }
        }
    }
}