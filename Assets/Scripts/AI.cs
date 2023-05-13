using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace TagTag
{
    public class AI : Brain
    {
        [SerializeField] private Vector3Int targetIndex;
        [SerializeField] private bool       _isTargetReached = false;
        [SerializeField] private bool       _isTargetSet     = false;

        private Queue<Vector3Int> _path = new();

        private readonly List<Vector3Int> _directions = new()
        {
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.left,
            Vector3Int.right
        };


        public void SetTarget(Vector3Int tIndex)
        {
            _isTargetReached = false;
            targetIndex      = tIndex;
            _path            = new();
            FindPath();
            _isTargetSet = true;
        }

        private void FindPath()
        {
            Queue<Vector3Int> queue = new();
            queue.Enqueue(CurrentIndex);

            Dictionary<Vector3Int, Vector3Int> cameFrom = new();
            cameFrom[CurrentIndex] = CurrentIndex;
            while (queue.Count > 0)
            {
                Vector3Int current = queue.Dequeue();
                if (current == targetIndex)
                {
                    ConstructPath(cameFrom);
                }

                foreach (Vector3Int direction in _directions)
                {
                    Vector3Int next = current + direction;
                    if (!Grid.CheckGridIndex(next) || cameFrom.ContainsKey(next))
                    {
                        continue;
                    }

                    queue.Enqueue(next);
                    cameFrom[next] = current;
                }
            }
        }

        private void ConstructPath(Dictionary<Vector3Int, Vector3Int> cameFrom)
        {
            List<Vector3Int> path    = new();
            Vector3Int       current = targetIndex;
            while (current != CurrentIndex)
            {
                path.Add(current);
                current = cameFrom[current];
            }

            path.Reverse();

            foreach (Vector3Int p in path)
            {
                _path.Enqueue(p);
            }
        }

        private void MoveToTarget()
        {
            if (_path.TryDequeue(out Vector3Int nextIndex))
            {
                MoveCharacterToPosition(Grid.TileMap.CellToWorld(nextIndex));
            }
            else
            {
                _isTargetReached = true;
                BrainManager.DestinationReached(this);
            }
        }

        protected override void Update()
        {
            if (!_isTargetSet || _isTargetReached || !MovementEnabled) return;
            base.Update();
            if (ElapsedTime > PollingRate)
            {
                ElapsedTime = 0f;
                MoveToTarget();
            }
        }
    }
}