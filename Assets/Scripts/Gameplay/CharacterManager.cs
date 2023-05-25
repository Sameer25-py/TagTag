using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private List<Character> characters;

        private void SpawnCharactersAtRandomPoints()
        {
            foreach (Character c in characters)
            {
                SpawnCharacterAtRandomPoint(c);
            }
        }

        private void SpawnCharacterAtRandomPoint(Character character)
        {
            Vector2 randomPoint =
                RandomPointGenerator.GetRandomPointOnMap();

            character.transform.position = randomPoint;
        }

        private void SelectRandomCharacterToInfect()
        {
            characters[Random.Range(0,characters.Count)].InfectCharacter();
        }

        private void Start()
        {
            SpawnCharactersAtRandomPoints();
            SelectRandomCharacterToInfect();
        }
    }
}