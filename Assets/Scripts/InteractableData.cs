using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TagTag
{
    [Serializable]
    [CreateAssetMenu(fileName = "Interactable", menuName = "Interactable", order = 0)]
    public class InteractableData : ScriptableObject
    {
        public Tile    Sprite;
        public int     SpawnCount = 2;
        public Vector3 scale      = Vector3.one;
    }
}