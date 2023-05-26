using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private List<Character> characters;

        public static            Action<Character> CharacterDestroyed;
        public static            Action<Character> InfectRandomCharacterExcept;
        [SerializeField] private Transform         randomLocations;

        public List<Character> GetCharacters => characters;

        private void SpawnCharactersAtRandomPoints()
        {
            foreach (Character c in characters)
            {
                SpawnCharacterAtRandomPoint(c);
            }
        }

        private void SpawnCharacterAtRandomPoint(Character character)
        {
            Vector2 randomPoint = randomLocations.GetChild(Random.Range(0, randomLocations.childCount))
                .position;

            character.transform.position = randomPoint;
        }

        private void SelectRandomCharacterToInfect(Character characterToExclude = null)
        {
            if (characterToExclude)
            {
                Character c = characters[Random.Range(0, characters.Count)];
                if (c == characterToExclude)
                {
                    SelectRandomCharacterToInfect(characterToExclude);
                }
                else
                {
                    c.InfectCharacter();
                }
            }
            else
            {
                characters[Random.Range(0, characters.Count)]
                    .InfectCharacter();
            }
        }

        public void ChangeCharactersMovementStatus(bool status)
        {
            foreach (Character c in characters)
            {
                c.EnableMovement = status;
            }
        }

        private void RemoveInfectionFromAllCharacters()
        {
            foreach (Character c in characters)
            {
                c.UnInfectCharacter();
            }
        }

        private IEnumerator InitializeRoundRoutine()
        {
            SpawnCharactersAtRandomPoints();
            RemoveInfectionFromAllCharacters();
            SelectRandomCharacterToInfect();
            ChangeCharactersMovementStatus(false);
            yield return new WaitForSeconds(1f);
            ChangeCharactersMovementStatus(true);
        }

        public void InitializeRound()
        {
            StartCoroutine(InitializeRoundRoutine());
        }

        private void OnEnable()
        {
            CharacterDestroyed          += OnCharacterDestroyed;
            InfectRandomCharacterExcept += SelectRandomCharacterToInfect;
        }

        private void OnDisable()
        {
            CharacterDestroyed          -= OnCharacterDestroyed;
            InfectRandomCharacterExcept -= SelectRandomCharacterToInfect;
        }

        private void OnCharacterDestroyed(Character obj)
        {
            characters.Remove(obj);
        }
    }
}