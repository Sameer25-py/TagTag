using System;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public List<Round>        Rounds;
        public BrainManager       BrainManager;
        public InteractionManager InteractionManager;
        public GameObject         Menu;

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
            StartRound(Rounds[1]);
        }

        private void StartRound(Round round)
        {   
            RoundText.SetRound(round.Index);
            InteractionManager.SetRound(round);
            Timer.StartTimer(round.Time);
            BrainManager.InitializeBrains();
            BrainManager.ChangeBrainsMovementStatus(true);
        }

        private void StartRoundWithDelay()
        {
            CurrentRound += 1;
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
            BrainManager.ChangeBrainsMovementStatus(false);
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
            BrainManager.ChangeBrainsMovementStatus(false);
        }

        public void HomeButton()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void ResumeButton()
        {
            Menu.SetActive(false);
            Timer.ResumeTimer();
            BrainManager.ChangeBrainsMovementStatus(true);
        }

        public void GameEnd()
        {
            PauseTitle.text       = "game over";
            PauseDescription.text = "ai won";
            BackButton.SetActive(false);
            Menu.SetActive(true);
            
        }
    }
}