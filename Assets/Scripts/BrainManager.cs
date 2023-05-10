using System.Collections.Generic;
using UnityEngine;

namespace TagTag
{
    public class BrainManager : MonoBehaviour
    {
        [SerializeField] private List<Brain> Brains;
        [SerializeField] private int         randomIndexTries = 5;
        
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

        private void Start()
        {
            PlaceCharactersAtValidIndices();
        }
        
        
        
    }
}