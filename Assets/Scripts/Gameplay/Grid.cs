using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Gameplay.GridInfo;

namespace Gameplay
{
    public class Grid : MonoBehaviour
    {
        public static          Tilemap    TileMap;
        public                 GameObject obj;
        public static readonly string     WallTile         = "Wall";
        private static         int        randomIndexTries = 5;

        private void Awake()
        {
            TileMap = GetComponent<Tilemap>();
        }

        public static bool CheckGridIndex(Vector3Int index)
        {
            Sprite sprite = TileMap.GetSprite(index);
            return (index.x >= MinRowIndex    && index.x < MaxRowIndex)    &&
                   (index.y >= MinColumnIndex && index.y < MaxColumnIndex) &&
                   !sprite.name.Contains(WallTile);
        }

        public static Vector3Int GetRandomValidIndex()
        {
            for (int i = 0; i < randomIndexTries; i++)
            {
                Vector3Int randomIndex = GetRandomIndexInGrid();
                if (CheckGridIndex(randomIndex))
                {
                    return randomIndex;
                }
            }

            return new();
        }

        private IEnumerator TraverseGrid()
        {
            for (int i = MinColumnIndex; i < MaxColumnIndex; i++)
            {
                for (int j = MinRowIndex; j < MaxRowIndex; j++)
                {
                    if (!CheckGridIndex(new Vector3Int(j, i))) continue;
                    Vector3 pos = TileMap.CellToWorld(new Vector3Int(j, i));
                    obj.transform.position = pos + TileMap.cellSize / 2f;

                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }
}