using System;
using UnityEngine;

namespace TagTag
{
    public class Character : MonoBehaviour, IInfect
    {
        public  SpriteRenderer SpriteRenderer;
        private Color          _defaultColor;
        private Collider2D     _collider2D;
        public  Color          InfectedColor = Color.red;

        [SerializeField] private bool _isInfected = false;

        private int _infectedDescrId;

        private void OnEnable()
        {
            _collider2D    = GetComponent<BoxCollider2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            _defaultColor  = SpriteRenderer.color;
        }

        private void EnableCollider()
        {
            if (_collider2D)
            {
                _collider2D.enabled = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!_isInfected) return;
            UnInfectCharacter();

            col.gameObject.GetComponent<Brain>()
                .InfectBrain();
        }

        public void InfectCharacter()
        {
            LTDescr descr = LeanTween.color(gameObject, InfectedColor, 0.5f)
                .setLoopPingPong(-1)
                .setEaseInOutBounce();
            _infectedDescrId    = descr.id;
            _isInfected         = true;
            _collider2D.enabled = false;
            Invoke(nameof(EnableCollider), 1f);
        }

        public void UnInfectCharacter()
        {
            LeanTween.cancel(_infectedDescrId);
            _isInfected          = false;
            SpriteRenderer.color = _defaultColor;
            _collider2D.enabled  = false;
            Invoke(nameof(EnableCollider), 1f);
        }

        public void BlastCharacter()
        {
            throw new NotImplementedException();
        }
    }
}