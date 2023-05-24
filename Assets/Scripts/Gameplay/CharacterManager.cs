using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class CharacterManager : MonoBehaviour
    {
        private Camera _mainCamera;

        [SerializeField] private List<Character> characters;

        private void OnEnable()
        {
            _mainCamera = Camera.main;
        }

        private void SpawnCharacterAtRandomPoint(Character character)
        {
            Vector2 randomPoint =
                RandomPointGenerator.GetRandomPointOnMap();

            character.transform.position = randomPoint;

        }

        private void Start()
        {
            SpawnCharacterAtRandomPoint(characters[0]);
        }
    }
}