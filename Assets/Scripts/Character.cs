using UnityEngine;

namespace TagTag
{
    public class Character : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;

        protected virtual void OnEnable()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}