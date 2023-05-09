using System;
using UnityEngine;

namespace TagTag
{
    public class Character : MonoBehaviour
    {
        public Vector3Int     CurrentIndex;
        public SpriteRenderer SpriteRenderer;

        protected virtual void OnEnable()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            CurrentIndex   = Grid.TileMap.WorldToCell(transform.position);
        }
    }
}