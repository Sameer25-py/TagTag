using System;
using UnityEngine;

namespace TagTag
{
    public class Brain : MonoBehaviour, IInfect
    {
        [SerializeField] protected Character  Character;
        [SerializeField] protected float      PollingRate = 0.5f;
        public                     Vector3Int CurrentIndex;
        private                    Transform  _characterTransform;
        public                     bool       IsInfected  = false;
        protected                  float      ElapsedTime = 0f;

        public void InfectCharacter()
        {
            IsInfected = true;
        }

        public void UnInfectCharacter()
        {
            IsInfected = false;
        }

        public void BlastCharacter()
        {
            IsInfected = false;
        }

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

        protected virtual void Update()
        {
            ElapsedTime += Time.deltaTime;
        }
    }
}