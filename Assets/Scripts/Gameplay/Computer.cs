using Pathfinding;
using UnityEngine;


namespace Gameplay
{
    public class Computer : Character
    {
        public  float NextWayPointDistance = 0.3f;
        private Path  path;
        private int   currentWayPoint  = 0;
        private bool  reachedEndOfPath = false;

        private Seeker seeker;

        protected override void OnEnable()
        {
            base.OnEnable();
            seeker = GetComponent<Seeker>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(GeneratePath), 0f, 1.5f);
        }


        private void GeneratePath()
        {
            if (!IsInfected)
            {
                seeker.StartPath(transform.position, RandomPointGenerator.GetRandomPointOnMap(), OnPathComplete);
            }
            else
            {
                Character[] chrs                  = FindObjectsOfType<Character>();
                float       minDistance           = -1f;
                Character   candidateFollowTarget = null;
                foreach (var chr in chrs)
                {
                    if (chr == this) continue;
                    float distance = Vector2.Distance(chr.transform.position, transform.position);
                    if (distance < minDistance)
                    {
                        minDistance           = distance;
                        candidateFollowTarget = chr;
                    }
                }

                if (!candidateFollowTarget)
                {
                    seeker.StartPath(transform.position, RandomPointGenerator.GetRandomPointOnMap(), OnPathComplete);
                }
                else
                {
                    seeker.StartPath(transform.position, candidateFollowTarget.transform.position, OnPathComplete);
                }
            }
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                path            = p;
                currentWayPoint = 0;
            }
        }

        private void FixedUpdate()
        {
            if (path == null) return;
            if (currentWayPoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
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