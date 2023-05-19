using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class BrainManager : MonoBehaviour
    {
        [SerializeField] private List<Brain> Brains;
        [SerializeField] private int         IndexTries = 5;

        public static Action<AI>    DestinationReached;
        public static Action<Brain> BrainDestroyed;
        public static Action<Brain> UpdateInfectedBrain;
        public static Action<Brain> InfectRandomBrain;

        public Brain InfectedBrain;

        public void ChangeBrainsMovementStatus(bool status)
        {
            foreach (Brain brain in Brains)
            {
                if (brain)
                {
                    brain.ChangeMovementStatus(status);
                }
            }
        }

        public void OnInfectRandomBrainCalled(Brain brainToExculde)
        {
            Brain randomBrain = Brains[Random.Range(0, Brains.Count)];
            if (randomBrain == brainToExculde)
            {
                OnInfectRandomBrainCalled(brainToExculde);
            }
            else
            {
                randomBrain.InfectBrain();
            }
        }

        private void PlaceCharactersAtValidIndices()
        {
            List<Vector3Int> cachedIndices = new();
            foreach (Brain brain in Brains)
            {
                for (int i = 0; i < IndexTries; i++)
                {
                    Vector3Int randomIndex = Grid.GetRandomValidIndex();
                    if (!cachedIndices.Contains(randomIndex))
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
                        aiBrain.SetTarget(Grid.GetRandomValidIndex());
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
            Brains[Random.Range(0, Brains.Count)]
                .InfectBrain();
        }

        private void RemoveInfectionFromAllBrains()
        {
            foreach (Brain brain in Brains)
            {
                if (brain)
                {
                    if (brain.TryGetComponent(out InfectedCollider col))
                    {
                        Destroy(col);
                        brain.UnInfectBrain();
                    }
                }
            }
        }

        private void OnEnable()
        {
            DestinationReached  += OnAITargetReached;
            BrainDestroyed      += OnBrainDestroyed;
            UpdateInfectedBrain += OnInfectedBrainUpdated;
            InfectRandomBrain   += OnInfectRandomBrainCalled;
        }

        private void OnInfectedBrainUpdated(Brain obj)
        {
            if (obj)
            {
                InfectedBrain = obj;
                //AssignAIBrainDestinations();
            }
        }

        private void OnBrainDestroyed(Brain obj)
        {
            if (Brains.Contains(obj))
            {
                Brains.Remove(obj);
            }
        }

        public void InitializeBrains()
        {
            RemoveInfectionFromAllBrains();
            PlaceCharactersAtValidIndices();
            SetRandomBrainToInfect();
            AssignAIBrainDestinations();
        }

        private void OnDisable()
        {
            DestinationReached  -= OnAITargetReached;
            BrainDestroyed      -= OnBrainDestroyed;
            UpdateInfectedBrain -= OnInfectedBrainUpdated;
            InfectRandomBrain   -= OnInfectRandomBrainCalled;
        }

        private void OnAITargetReached(AI obj)
        {
            if (!obj) return;
            if (obj != InfectedBrain)
            {
                obj.SetTarget(Grid.GetRandomValidIndex());
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