using System;
using System.Collections;
using UnityEngine;

namespace TagTag
{
    public abstract class Brain : MonoBehaviour
    {
        [SerializeField] protected Character  Character;
        [SerializeField] protected float      PollingRate                  = 0.5f;
        [SerializeField] protected float      InflictInfectionImmunityTime = 1f;
        [SerializeField] protected bool       MovementAvailable            = true;
        public                     Vector3Int CurrentIndex;
        private                    Transform  _characterTransform;
        protected                  float      ElapsedTime = 0f;

        private IEnumerator ChangeMovementSpeedRoutine(float speedFactor, float duration)
        {
            float _cachedPollingRate = PollingRate;
            PollingRate *= speedFactor;
            yield return new WaitForSeconds(duration);
            PollingRate = _cachedPollingRate;
        }

        private IEnumerator ChangeMovementAvailableRoutine(float duration)
        {
            MovementAvailable = false;
            yield return new WaitForSeconds(duration);
            MovementAvailable = true;
        }

        public void ChangeMovementSpeedForTime(float speedFactor, float duration)
        {
            StartCoroutine(ChangeMovementSpeedRoutine(speedFactor, duration));
        }

        public void ChangeMovementAvailableStatusForTime(float duration)
        {
            StartCoroutine(ChangeMovementAvailableRoutine(duration));
        }

        private void AttachInfectedCollider()
        {
            gameObject.AddComponent<InfectedCollider>();
        }

        private void EnableMovement()
        {
            MovementAvailable = true;
        }

        public virtual void InfectBrain()
        {
            MovementAvailable = false;
            ElapsedTime       = 0f;
            Invoke(nameof(EnableMovement), InflictInfectionImmunityTime);
            Character.InfectCharacter();
            BrainManager.UpdateInfectedBrain(this);
            Invoke(nameof(AttachInfectedCollider), InflictInfectionImmunityTime);
        }

        protected void OnEnable()
        {
            _characterTransform = Character.transform;
            MovementAvailable   = true;
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
                LerpCharacterToPosition(Grid.TileMap.CellToWorld(nextIndex));
            }
        }

        public void MoveCharacterToPosition(Vector3 position)
        {
            _characterTransform.position = position + Grid.TileMap.cellSize / 2f;
            UpdateIndex(position);
        }

        protected virtual void LerpCharacterToPosition(Vector3 position)
        {
            if (_characterTransform)
            {
                LeanTween.move(_characterTransform.gameObject, position + Grid.TileMap.cellSize / 2f, PollingRate)
                    .setOnComplete(
                        () => { UpdateIndex(position); })
                    .setEaseLinear();
            }
        }

        protected virtual void OnIndexUpdated()
        {
            InteractionManager.IndexUpdated(this);
        }

        protected virtual void Update()
        {
            if (MovementAvailable)
            {
                ElapsedTime += Time.deltaTime;
            }
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