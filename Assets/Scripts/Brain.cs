using System;
using UnityEngine;

namespace TagTag
{
    public class Brain : MonoBehaviour
    {
        [SerializeField] protected Character  Character;
        public                     Vector3Int CurrentIndex;
        private                    Transform  _characterTransform;

        protected void OnEnable()
        {
            _characterTransform = Character.transform;
        }
        
        protected virtual void UpdateIndex(Vector3 position)
        {
            CurrentIndex = Grid.TileMap.WorldToCell(position);
        }

        protected virtual void MoveInDirection(Vector3Int direction)
        {
            Vector3Int nextIndex = CurrentIndex + direction;
            if (Grid.CheckGridIndex(nextIndex))
            {
                MoveCharacterToPosition(Grid.TileMap.CellToWorld(nextIndex));
            }
            
        }

        public void MoveCharacterToPosition(Vector3 position)
        {
            if (_characterTransform)
            {
                _characterTransform.position = position + Grid.TileMap.cellSize / 2f;
                UpdateIndex(position);
            }
        }
    }
}