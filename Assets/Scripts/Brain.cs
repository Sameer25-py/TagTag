using System;
using UnityEngine;

namespace TagTag
{
    public abstract class Brain : MonoBehaviour
    {
        [SerializeField] protected Character  Character;
        [SerializeField] protected float      PollingRate                  = 0.5f;
        [SerializeField] protected float      InflictInfectionImmunityTime = 1f;
        public                     Vector3Int CurrentIndex;
        private                    Transform  _characterTransform;
        protected                  float      ElapsedTime = 0f;


        private void AttachInfectedCollider()
        {
            gameObject.AddComponent<InfectedCollider>();
        }

        public virtual void InfectBrain()
        {
            Character.InfectCharacter();
            BrainManager.UpdateInfectedBrain(this);
            Invoke(nameof(AttachInfectedCollider), InflictInfectionImmunityTime);
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