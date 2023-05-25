using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private List<Character> characters;

        public static Action<Character> CharacterDestroyed;

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
            Vector2 randomPoint =
                RandomPointGenerator.GetRandomPointOnMap();

            character.transform.position = randomPoint;
        }

        private void SelectRandomCharacterToInfect()
        {
            characters[Random.Range(0, characters.Count)]
                .InfectCharacter();
        }

        public void ChangeCharactersMovementStatus(bool status)
        {
            foreach (Character c in characters)
            {
                c.EnableMovement = status;
            }
        }

        private IEnumerator InitializeRoundRoutine()
        {
            SpawnCharactersAtRandomPoints();
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
            CharacterDestroyed += OnCharacterDestroyed;
        }

        private void OnDisable()
        {
            CharacterDestroyed -= OnCharacterDestroyed;
        }

        private void OnCharacterDestroyed(Character obj)
        {
            characters.Remove(obj);
        }
    }
}