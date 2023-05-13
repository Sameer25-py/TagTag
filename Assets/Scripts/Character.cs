using System;
using UnityEngine;

namespace TagTag
{
    public class Character : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;

        private void OnEnable()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
        }
    }
}