using System;
using System.Collections.Generic;
using Gameplay.Grid;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public List<Round>         Rounds;
        public GameObject          Menu;
        public CharacterManager    CharacterManager;
        public InteractableManager InteractableManager;

        public GameObject BackButton;
        public TMP_Text   PauseTitle, PauseDescription;

        public int       CurrentRound    = 1;
        public int       MaxAllowedRound = 3;
        public Timer     Timer;
        public RoundText RoundText;

        public static Action RoundStarted;

        private void OnEnable()
        {
            Timer.TimerEnd += EndRound;
        }

        private void OnDisable()
        {
            Timer.TimerEnd -= EndRound;
        }

        private void Start()
        {
            CurrentRound = 1;
            StartRound(Rounds[0]);
        }

        private void StartRound(Round round)
        {
            RoundText.SetRound(round.Index);
            CharacterManager.InitializeRound();
            InteractableManager.Initialize(round);
            Timer.StartTimer(round.Time);
        }

        private void StartRoundWithDelay()
        {
            CurrentRound += 1;
            if (CurrentRound > MaxAllowedRound)
            {
                GameEnd();
            }

            foreach (Round round in Rounds)
            {
                if (round.Index == CurrentRound)
                {
                    StartRound(round);
                }
            }
        }

        private void EndRound()
        {
            CharacterManager.ChangeCharactersMovementStatus(false);
            Invoke(nameof(StartRoundWithDelay), 1f);
        }

        public void RestartButton()
        {
            StartRound(Rounds[0]);
            Menu.SetActive(false);
        }

        public void PauseButton()
        {
            PauseTitle.text       = "pause";
            PauseDescription.text = "";
            BackButton.SetActive(true);
            Menu.SetActive(true);
            Timer.PauseTimer();
            CharacterManager.ChangeCharactersMovementStatus(false);
        }

        public void HomeButton()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void ResumeButton()
        {
            Menu.SetActive(false);
            Timer.ResumeTimer();
            CharacterManager.ChangeCharactersMovementStatus(true);
        }

        private void GameEnd()
        {
            PauseTitle.text = "game over";
            Character c = CharacterManager.GetCharacters[0];
            if (c is Computer)
            {
                PauseDescription.text = "ai won";
            }
            else
            {
                PauseDescription.text = "human won";
            }

            BackButton.SetActive(false);
            Menu.SetActive(true);
        }
    }
}