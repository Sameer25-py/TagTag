using UnityEngine;

namespace Gameplay
{
    public class Character : MonoBehaviour, IInfect
    {
        public  SpriteRenderer SpriteRenderer;
        private Color          _defaultColor;
        public  Color          InfectedColor = Color.red;


        private int _infectedDescrId;

        private void OnEnable()
        {
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
                Destroy(col);
            }
        }

        public void BlastCharacter()
        {
            if (TryGetComponent(out InfectedCollider _))
            {
                Destroy(gameObject);
                LeanTween.cancel(_infectedDescrId);
            }
        }
    }
}