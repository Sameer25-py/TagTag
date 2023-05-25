using Gameplay.Grid;
using UnityEngine;

namespace Gameplay
{
    public class InfectedCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent(out Gameplay.Character c))
            {
                c.InfectCharacter();
                GetComponent<Character>()
                    .UnInfectCharacter();
            }
        }
    }
}