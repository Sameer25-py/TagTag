using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "Round", menuName = "Round", order = 0)]
    public class Round : ScriptableObject
    {
        public int   Index;
        public float Time;

        public List<InteractableData> AllowedInteractables;
    }
}