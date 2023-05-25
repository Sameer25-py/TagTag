using System;
using Gameplay.Grid;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gameplay
{
    public class RandomPointGenerator : MonoBehaviour
    {
        private static Tilemap tileMap;
        private static int     tries = 100;

        private void OnEnable()
        {
            tileMap = GetComponent<Tilemap>();
        }

        public static Vector2 GetRandomPointOnMap()
        {
            for (int i = 0; i < tries; i++)
            {
                Vector3Int randomIndex = GridInfo.GetRandomIndexInGrid();
                if (tileMap.HasTile(randomIndex))
                {
                    return tileMap.CellToWorld(randomIndex);
                }
            }


            return Vector2.zero;
        }
    }
}