using System;
using Gameplay.Grid;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gameplay
{
    public class RandomPointGenerator : MonoBehaviour
    {
        private static Tilemap  tileMap;

        private void OnEnable()
        {
            tileMap = GetComponent<Tilemap>();
        }

        public static Vector2 GetRandomPointOnMap()
        {
            Vector3Int randomIndex = GridInfo.GetRandomIndexInGrid();
            return tileMap.CellToWorld(randomIndex);
        }
    }
}