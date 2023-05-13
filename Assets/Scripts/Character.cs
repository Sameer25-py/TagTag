using System;
using UnityEngine;

namespace TagTag
{
    public class Character : MonoBehaviour,IInfect
    {
        public SpriteRenderer SpriteRenderer;

        private void OnEnable()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
        }

        public void InfectCharacter()
        {
            throw new NotImplementedException();
        }

        public void UnInfectCharacter()
        {
            throw new NotImplementedException();
        }

        public void BlastCharacter()
        {
            throw new NotImplementedException();
        }
    }
}