using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class Character : MonoBehaviour, IInfect
    {
        public                     float          Speed = 1.5f;
        protected                  Rigidbody2D    Rb2D;
        [SerializeField] protected LayerMask      LayerMask;
        public                     bool           EnableMovement = true;
        [SerializeField] protected Color          InfectedColor  = Color.red;
        [SerializeField] protected Vector2        MoveDirection  = Vector2.zero;
        private                    SpriteRenderer SpriteRenderer;
        private                    AudioSource    _audioSource;
        private                    Color          _defaultColor;
        public                     AudioClip      Tag;
        private                    int            _infectedDescrId;
        private                    Animator       _animator;
        private static readonly    int            s_moveLeft = Animator.StringToHash("moveLeft");
        private static readonly    int            s_moveUp   = Animator.StringToHash("moveUp");
        private static readonly    int            s_isMoving = Animator.StringToHash("isMoving");
        public                     bool           IsInfected = false;

        private Vector2       currentPosition;
        private Vector2       previousPosition;
        private bool          moveLeft;
        private bool          moveUp;
        private bool          isMoving;
        private BoxCollider2D _collider2D;

        [SerializeField] private bool  canInfect;
        [SerializeField] private float infectionDelay = 2f;

        protected virtual void OnEnable()
        {
            Rb2D           =  GetComponent<Rigidbody2D>();
            _audioSource   =  GetComponent<AudioSource>();
            SpriteRenderer =  GetComponent<SpriteRenderer>();
            _defaultColor  =  SpriteRenderer.color;
            _collider2D    =  GetComponent<BoxCollider2D>();
            Timer.TimerEnd += BlastCharacter;
            _animator      =  GetComponent<Animator>();
        }

        protected void OnDisable()
        {
            Timer.TimerEnd -= BlastCharacter;
        }

        protected virtual void Update()
        {
            CheckMovement();
        }

        private void CheckMovement()
        {
            currentPosition = transform.position;

            if (currentPosition != previousPosition)
            {
                // The position has changed
                CheckMovementDirection();
            }

            previousPosition = currentPosition;
        }

        void CheckMovementDirection()
        {
            Vector3 direction = currentPosition - previousPosition;

            if (direction.magnitude < 0.01f)
            {
                // The player has moved very little (likely stopped)
                // Handle accordingly
                moveLeft = false;
                moveUp   = false;
                isMoving = false;
            }
            else
            {
                direction.Normalize();

                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    if (direction.x > 0)
                    {
                        // Player is moving right
                        moveLeft = false;
                        moveUp   = false;
                        isMoving = true;
                    }
                    else
                    {
                        // Player is moving left
                        moveLeft = true;
                        moveUp   = false;
                        isMoving = true;
                    }
                }
                else
                {
                    if (direction.y > 0)
                    {
                        // Player is moving up
                        moveLeft = false;
                        moveUp   = true;
                        isMoving = true;
                    }
                    else
                    {
                        // Player is moving down
                        moveLeft = false;
                        moveUp   = false;
                        isMoving = true;
                    }
                }
            }

            UpdateAnimation(moveLeft, moveUp, isMoving);
        }

        void UpdateAnimation(bool moveLeft, bool moveUp, bool isMoving)
        {
            _animator.SetBool(s_moveLeft, moveLeft);
            _animator.SetBool(s_moveUp, moveUp);
            _animator.SetBool(s_isMoving, isMoving);
        }

        private void EnableMovementAndInfectionAbilty()
        {
            canInfect      = true;
            EnableMovement = true;
            //TODO:intoduce variable
            Speed          = 1.5f;

        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Character c))
            {
                if (IsInfected && canInfect && !c.IsInfected)
                {
                    c.InfectCharacter();
                    UnInfectCharacter();
                }
            }
        }

        public void InfectCharacter()
        {
            IsInfected     = true;
            canInfect      = false;
            EnableMovement = false;
            Speed          = 0f;
            Invoke(nameof(EnableMovementAndInfectionAbilty), infectionDelay);
            LTDescr descr = LeanTween.color(gameObject, InfectedColor, 0.5f)
                .setLoopPingPong(-1)
                .setEaseInOutBounce();
            _infectedDescrId = descr.id;
        }

        public void UnInfectCharacter()
        {
            IsInfected = false;
            canInfect  = false;
            LeanTween.cancel(_infectedDescrId);
            SpriteRenderer.color = _defaultColor;
            _audioSource.PlayOneShot(Tag);
        }

        public void BlastCharacter()
        {
            if (IsInfected)
            {
                AudioManager.PlayExplosionSound();
                LeanTween.cancel(_infectedDescrId);
                Destroy(gameObject);
            }
        }

        protected void OnDestroy()
        {
            CharacterManager.CharacterDestroyed?.Invoke(this);
        }

        private IEnumerator ChangeSpeedRoutine(float speed, float time)
        {
            float _cachedSpeed = Speed;
            Speed = speed;
            yield return new WaitForSeconds(time);
            Speed = _cachedSpeed;
        }

        public void ChangeSpeedForTime(float speed, float time)
        {
            StartCoroutine(ChangeSpeedRoutine(speed, time));
        }

        private IEnumerator ChangeEnableMovementRoutine(float time, bool status)
        {
            bool  _cachedEnableMovment = EnableMovement;
            float _cachedspeed         = Speed;
            EnableMovement = status;
            Speed          = 0f;
            yield return new WaitForSeconds(time);
            EnableMovement = _cachedEnableMovment;
            Speed          = _cachedspeed;
        }

        public void ChangeEnableMovemntForTime(float time, bool status)
        {
            StartCoroutine(ChangeEnableMovementRoutine(time, status));
        }
    }
}