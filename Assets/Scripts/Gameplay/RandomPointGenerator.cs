using System;
using Gameplay.Grid;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gameplay
{
    public class RandomPointGenerator : MonoBehaviour
    {
        private static Tilemap   tileMap;
        private static int       tries = 100;
        private static Transform Transform;

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
    }
}