using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TagTag
{
    public class EnvironmentCreator : MonoBehaviour
    {
        public GameObject Sprite;

        private void Start()
        {

            var tileMap = GetComponent<Tilemap>();
            var x = tileMap.layoutGrid.CellToLocal(new Vector3Int(0,0,0));

            Sprite.transform.position = x + tileMap.tileAnchor;
        }
    }
}