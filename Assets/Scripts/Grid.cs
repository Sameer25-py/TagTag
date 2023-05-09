using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static TagTag.GridInfo;

namespace TagTag
{
    public class Grid : MonoBehaviour
    {
        public static Tilemap    TileMap;
        public        GameObject obj;

        private void Start()
        {
            TileMap                = GetComponent<Tilemap>();
            obj.transform.position = TileMap.CellToWorld(new Vector3Int(4,-2));
            //StartCoroutine(TraverseGrid());
        }


        private IEnumerator TraverseGrid()
        {
            for (int i = MinColumnIndex; i < MaxColumnIndex; i++)
            {
                for (int j = MinRowIndex; j < MaxRowIndex; j++)
                {
                    Vector3 pos = TileMap.CellToWorld(new Vector3Int(j, i));
                    obj.transform.position = pos + TileMap.cellSize / 2f;

                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }
}