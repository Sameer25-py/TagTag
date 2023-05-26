using System.Collections;
using Pathfinding;
using UnityEngine;


namespace Gameplay
{
    public class Computer : Character
    {
        public  float     NextWayPointDistance = 0.1f;
        private Path      path;
        private int       currentWayPoint  = 0;
        private bool      reachedEndOfPath = false;
        private Transform _cachedFollowTransform;

        [SerializeField] private Transform randomLocations;

        private Coroutine _creatPathCoroutine;

        private Seeker seeker;

        protected override void OnEnable()
        {
            base.OnEnable();
            GameManager.RoundStarted += OnNewRoundStarted;
            seeker                   =  GetComponent<Seeker>();
        }

        private void OnNewRoundStarted()
        {
            path             = null;
            currentWayPoint  = 0;
            reachedEndOfPath = true;
        }

        public void StartSeekBehavior()
        {
            if (IsInfected)
            {
                SeekRandomCharacter();
            }
            else
            {
                SeekRandomLocation();
            }

            seeker.StartPath(transform.position, _cachedFollowTransform.position, OnPathComplete);
        }

        private void SeekRandomLocation()
        {
            Transform randomLocation = randomLocations.GetChild(Random.Range(0, randomLocations.childCount));
            _cachedFollowTransform = randomLocation;
        }

        private void SeekRandomCharacter()
        {
            Character[] chr = FindObjectsOfType<Character>(false);
            for (int i = 0; i < 10; i++)
            {
                Character randomChr = chr[Random.Range(0, chr.Length)];
                if (randomChr == this)
                {
                    continue;
                }

                _cachedFollowTransform = randomChr.transform;
                break;
            }
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                path             = p;
                currentWayPoint  = 0;
                reachedEndOfPath = false;
            }
        }

        private void FixedUpdate()
        {
            if (path == null) return;
            if (currentWayPoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                StartSeekBehavior();
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            MoveDirection = ((Vector2)path.vectorPath[currentWayPoint] - Rb2D.position).normalized;
            Vector2 newPosition = Rb2D.position                        + MoveDirection * (Time.deltaTime * Speed);
            Rb2D.MovePosition(newPosition);

            float distance = Vector2.Distance(Rb2D.position, path.vectorPath[currentWayPoint]);
            if (distance < NextWayPointDistance)
            {
                currentWayPoint += 1;
            }
        }
    }
}