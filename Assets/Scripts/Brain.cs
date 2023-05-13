using System;
using UnityEngine;

namespace TagTag
{
    public abstract class Brain : MonoBehaviour
    {
        [SerializeField] protected Character  Character;
        [SerializeField] protected float      PollingRate = 0.5f;
        public                     Vector3Int CurrentIndex;
        private                    Transform  _characterTransform;
        protected                  float      ElapsedTime     = 0f;
        [SerializeField] protected bool       MovementEnabled = true;


        protected virtual void EnableMovement()
        {
            MovementEnabled = true;
        }

        public void InfectBrain()
        {
            MovementEnabled = false;
            Invoke(nameof(EnableMovement), 1f);
            Character.InfectCharacter();
            BrainManager.UpdateInfectedBrain(this);
        }

        protected void OnEnable()
        {
            _characterTransform = Character.transform;
        }

        protected virtual void UpdateIndex(Vector3 position)
        {
            CurrentIndex = Grid.TileMap.WorldToCell(position);
            OnIndexUpdated();
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

        protected virtual void OnIndexUpdated() { }

        protected virtual void Update()
        {
            if (!MovementEnabled) return;
            ElapsedTime += Time.deltaTime;
        }

        protected virtual void OnDestroy()
        {
            if (BrainManager.BrainDestroyed != null)
            {
                BrainManager.BrainDestroyed(this);
            }
        }
    }
}