using UnityEngine;

namespace Gameplay.Grid
{
    public class Character : MonoBehaviour, IInfect
    {

        private Vector3 previousPosition;
        private Vector3 currentPosition;

        public bool isMoving = false;
        public bool moveLeft = false;
        public bool moveUp = false;
        public Animator animator;


        public SpriteRenderer SpriteRenderer;
        private Color          _defaultColor;
        public  Color          InfectedColor = Color.red;

        private AudioSource _audioSource;

        public AudioClip Tag;

        private int _infectedDescrId;

        private void OnEnable()
        {
            _audioSource   =  GetComponent<AudioSource>();
            SpriteRenderer =  GetComponent<SpriteRenderer>();
            _defaultColor  =  SpriteRenderer.color;
            Timer.TimerEnd += BlastCharacter;
        }

        private void OnDisable()
        {
            Timer.TimerEnd -= BlastCharacter;
        }

        private void Update()
        {
            CheckMovement();
        }

        void CheckMovement()
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
                moveUp = false;
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
                        moveUp = false;
                        isMoving = true;

                    }
                    else
                    {
                        // Player is moving left
                        moveLeft = true;
                        moveUp = false;
                        isMoving = true;
                    }
                }
                else
                {
                    if (direction.y > 0)
                    {
                        // Player is moving up
                        moveLeft = false;
                        moveUp = true;
                        isMoving = true;
                    }
                    else
                    {
                        // Player is moving down
                        moveLeft = false;
                        moveUp = false;
                        isMoving = true;
                    }
                }
            }

            UpdateAnimation(moveLeft, moveUp, isMoving);
        }

        void UpdateAnimation(bool moveLeft, bool moveUp, bool isMoving)
        {
            animator.SetBool("moveLeft", moveLeft);
            animator.SetBool("moveUp", moveUp);
            animator.SetBool("isMoving", isMoving);
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