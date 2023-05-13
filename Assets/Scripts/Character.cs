using System;
using UnityEngine;

namespace TagTag
{
    public class Character : MonoBehaviour, IInfect
    {
        public  SpriteRenderer SpriteRenderer;
        private Color          _defaultColor;
        private Rigidbody2D    _rb2D;
        public  Color          InfectedColor = Color.red;

        [SerializeField] private bool _isInfected = false;

        private int _infectedDescrId;

        private void OnEnable()
        {
            _rb2D          = GetComponent<Rigidbody2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            _defaultColor  = SpriteRenderer.color;
        }

        private void EnableCollider()
        {
            if (_rb2D)
            {
                _rb2D.simulated = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!_isInfected) return;

            _isInfected = false;
            UnInfectCharacter();

            col.gameObject.GetComponent<Brain>()
                .InfectBrain();
        }

        public void InfectCharacter()
        {
            var descr = LeanTween.color(gameObject, InfectedColor, 0.5f)
                .setLoopPingPong(-1)
                .setEaseInOutBounce();
            _infectedDescrId = descr.id;
            _isInfected      = true;
            _rb2D.simulated  = false;
            Invoke(nameof(EnableCollider), 1f);
        }

        public void UnInfectCharacter()
        {
            LeanTween.cancel(_infectedDescrId);
            _isInfected          = false;
            SpriteRenderer.color = _defaultColor;

            _rb2D.simulated = false;
            Invoke(nameof(EnableCollider), 1f);
        }

        public void BlastCharacter()
        {
            throw new NotImplementedException();
        }
    }
}