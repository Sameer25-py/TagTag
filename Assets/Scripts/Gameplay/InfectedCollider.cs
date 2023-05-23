using Gameplay.Grid;
using UnityEngine;

namespace Gameplay
{
    public class InfectedCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            GetComponent<Gameplay.Grid.Character>()
                .UnInfectCharacter();

            col.GetComponent<Brain>()
                .InfectBrain();

            Destroy(this);
        }
    }
}