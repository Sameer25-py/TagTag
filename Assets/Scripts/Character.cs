using System;
using UnityEngine;

namespace TagTag
{
    public class Character : MonoBehaviour, IInfect
    {
        public  SpriteRenderer SpriteRenderer;
        private Color          _defaultColor;
        public  Color          InfectedColor = Color.red;


        private int _infectedDescrId;

        private void OnEnable()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            _defaultColor  = SpriteRenderer.color;
        }

        public void InfectCharacter()
        {
            LTDescr descr = LeanTween.color(gameObject, InfectedColor, 0.5f)
                .setLoopPingPong(-1)
                .setEaseInOutBounce();
            _infectedDescrId = descr.id;
        }

        public void UnInfectCharacter()
        {
            LeanTween.cancel(_infectedDescrId);
            SpriteRenderer.color = _defaultColor;
        }

        public void BlastCharacter()
        {
            throw new NotImplementedException();
        }
    }
}