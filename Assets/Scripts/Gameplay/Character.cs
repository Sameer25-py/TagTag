using UnityEngine;

namespace Gameplay
{
    public class Character : MonoBehaviour, IInfect
    {
        public  SpriteRenderer SpriteRenderer;
        private Color          _defaultColor;
        public  Color          InfectedColor = Color.red;

        private AudioSource _audioSource;

        public AudioClip Tag;

        private int _infectedDescrId;

        private void OnEnable()
        {
            _audioSource   =  GetComponent<AudioSource>();
            SpriteRenderer =  GetComponent<SpriteRenderer>();
            _defaultColor  =  SpriteRenderer.color;
            Timer.TimerEnd += BlastCharacter;
        }

        private void OnDisable()
        {
            Timer.TimerEnd -= BlastCharacter;
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
            if (TryGetComponent(out InfectedCollider col))
            {
                _audioSource.PlayOneShot(Tag);
                Destroy(col);
            }
        }

        public void BlastCharacter()
        {
            if (TryGetComponent(out InfectedCollider _))
            {
                AudioManager.PlayExplosionSound();
                LeanTween.cancel(_infectedDescrId);
                Destroy(gameObject);
            }
        }
    }
}