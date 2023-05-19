using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gameplay
{
    [Serializable]
    [CreateAssetMenu(fileName = "Interactable", menuName = "Interactable", order = 0)]
    public class InteractableData : ScriptableObject
    {
        public string    Name;
        public Tile      Sprite;
        public int       SpawnCount = 2;
        public Vector3   scale      = Vector3.one;
        public AudioClip EffectClip;
    }
}