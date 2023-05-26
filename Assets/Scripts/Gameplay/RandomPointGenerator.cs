using System;
using Gameplay.Grid;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gameplay
{
    public class RandomPointGenerator : MonoBehaviour
    {
        private static Tilemap   tileMap;
        private static int       tries = 100;
        private static Transform Transform;

        private Vector2[] _randomPoints;

        private void OnEnable()
        {
            tileMap   = GetComponent<Tilemap>();
            Transform = transform;
        }

        public static Vector2 GetRandomPointOnMap(float obstacleAvoidanceRadius = 0.3f)
        {
            for (int i = 0; i < tries; i++)
            {
                Vector3Int randomIndex = GridInfo.GetRandomIndexInGrid();
                if (tileMap.HasTile(randomIndex))
                {
                    Vector2      randomPoint = tileMap.CellToWorld(randomIndex);
                    Collider2D[] colliders   = Physics2D.OverlapCircleAll(randomPoint, obstacleAvoidanceRadius);
                    if (colliders.Length == 0) return randomPoint;
                }
            }


            return Vector2.zero;
        }

        public static Vector2 GetRandomPoint()
        {
            return tileMap.CellToWorld(GridInfo.GetRandomIndexInGrid());
        }

        private void Start()
        {
            _randomPoints = new Vector2[100];

            for (int i = 0; i < 100; i++)
            {
                _randomPoints[i] = GetRandomPoint();
            }
        }
    }
}