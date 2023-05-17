using UnityEngine;

namespace Gameplay
{
    public class InfectedCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            GetComponent<Character>()
                .UnInfectCharacter();

            col.GetComponent<Brain>()
                .InfectBrain();

            Destroy(this);
        }
    }
}