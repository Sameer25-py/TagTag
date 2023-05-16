using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TagTag
{
    [CreateAssetMenu(fileName = "Round", menuName = "Round", order = 0)]
    public class Round : ScriptableObject
    {
        public int   Index;
        public float Time;

        public List<InteractableData> AllowedInteractables;
    }
}