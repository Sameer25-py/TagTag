using System.Collections.Generic;
using UnityEngine;

namespace TagTag
{
    public class AI : Brain
    {
        [SerializeField] private Vector2 targetPosition;
        [SerializeField] private bool    _isTargetReached = false;
        [SerializeField] private bool    _isTargetSet     = false;

        private List<Vector3Int> _path;


        public void SetTarget(Vector2 targetPostion)
        {
            _isTargetReached = false;
            targetPosition   = targetPostion;
            _path            = new();
            _isTargetSet     = true;
            
        }

        private List<Vector3Int> GetNeighboursIndices()
        {
            return new List<Vector3Int>()
            {
                new(CurrentIndex.x                 + 1, CurrentIndex.y),
                new(CurrentIndex.x, CurrentIndex.y + 1),
                new(CurrentIndex.x                 - 1, CurrentIndex.y),
                new(CurrentIndex.x, CurrentIndex.y - 1)
            };
        }

        private List<Vector3Int> GetValidNeighboursIndices()
        {
            List<Vector3Int> neighboursdIndices = GetNeighboursIndices();
            List<Vector3Int> validIndices       = new();
            foreach (Vector3Int index in neighboursdIndices)
            {
                if (Grid.CheckGridIndex(index))
                {
                    validIndices.Add(index);
                }
            }

            return validIndices;
        }

        private Vector3Int GetBestNeighbourIndex()
        {
            List<Vector3Int> validneighbours = GetValidNeighboursIndices();
            float            minDistance     = Mathf.Infinity;
            Vector3Int       bestNeighbour   = CurrentIndex;
            Vector3          currentPosition = Grid.TileMap.CellToWorld(CurrentIndex);
            Vector3Int       targetIndex     = Grid.TileMap.WorldToCell(targetPosition);
            foreach (Vector3Int validneighbour in validneighbours)
            {
                if (targetIndex == validneighbour)
                {
                    _isTargetReached = true;
                    return validneighbour;
                }

                if (_path.Contains(validneighbour))
                {
                    continue;
                }

                float distance = Vector2.Distance(currentPosition, targetPosition);
                if (distance < minDistance)
                {
                    minDistance   = distance;
                    bestNeighbour = validneighbour;
                }
            }

            return bestNeighbour;
        }

        private void MoveToTarget()
        {
            if (_isTargetReached)
            {
                return;
            }

            Vector3Int bestNeighbourIndex    = GetBestNeighbourIndex();
            Vector3    bestNeighBourPosition = Grid.TileMap.CellToWorld(bestNeighbourIndex);
            MoveCharacterToPosition(bestNeighBourPosition);
        }

        protected override void Update()
        {
            if (!_isTargetSet || _isTargetReached) return;
            base.Update();
            if (ElapsedTime > PollingRate)
            {
                ElapsedTime = 0f;
                MoveToTarget();
            }
        }
    }
}