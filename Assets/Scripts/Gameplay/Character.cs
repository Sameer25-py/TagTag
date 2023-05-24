using System;
using UnityEngine;

namespace Gameplay
{
    public class Character : MonoBehaviour, IInfect
    {
        public                     float          Speed = 1.5f;
        protected                  Rigidbody2D    Rb2D;
        [SerializeField] protected LayerMask      LayerMask;
        [SerializeField] protected bool           EnableMovement = true;
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

        private Vector2 currentPosition;
        private Vector2 previousPosition;
        private bool    moveLeft;
        private bool    moveUp;
        private bool    isMoving;

        protected virtual void OnEnable()
        {
            Rb2D           =  GetComponent<Rigidbody2D>();
            _audioSource   =  GetComponent<AudioSource>();
            SpriteRenderer =  GetComponent<SpriteRenderer>();
            _defaultColor  =  SpriteRenderer.color;
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

        public void InfectCharacter()
        {
            LTDescr descr = LeanTween.color(gameObject, InfectedColor, 0.5f)
                .setLoopPingPong(-1)
                .setEaseInOutBounce();
            _infectedDescrId = descr.id;
        }

        public void UnInfectCharacter()
        {
            LeanTween.cancel(_infectedDescrId);
            SpriteRenderer.color = _defaultColor;
            if (TryGetComponent(out InfectedCollider col))
            {
                _audioSource.PlayOneShot(Tag);
                Destroy(col);
            }
        }

        public void BlastCharacter()
        {
            if (TryGetComponent(out InfectedCollider _))
            {
                AudioManager.PlayExplosionSound();
                LeanTween.cancel(_infectedDescrId);
                Destroy(gameObject);
            }
        }
    }
}